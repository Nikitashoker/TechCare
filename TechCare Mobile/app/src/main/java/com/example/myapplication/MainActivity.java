package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class MainActivity extends AppCompatActivity {

    private static final String TAG = "SQL_CONNECTION_LOG";

    TextView TvRegLog;
    EditText EDEmail, EDLog, EDPass;
    Button btnAutorizz, btnRegOrLog;

    Connection conn;

    final String CONNECTION_STRING =
            "jdbc:jtds:sqlserver://192.168.31.180;databaseName=RepairServiceDB;user=nefor;password=27062006nik;";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // Привязываем элементы к объектам
        TvRegLog = findViewById(R.id.TVRIZZ);
        EDEmail = findViewById(R.id.EDEmail);
        EDLog = findViewById(R.id.EDLogin);
        EDPass = findViewById(R.id.EDPass);
        btnAutorizz = findViewById(R.id.btnRegLog);
        btnRegOrLog = findViewById(R.id.btnRegOrLog);

        // Изначально форма авторизации
        updateForm(true);

        // Обработка нажатия кнопки "сменить режим"
        btnRegOrLog.setOnClickListener(v -> switchAuthMode());

        // Обработка нажатия кнопки "Отправить"
        btnAutorizz.setOnClickListener(v -> submitForm());
    }

    // Переключение между режимами авторизации и регистрации
    private void switchAuthMode() {
        boolean currentIsAuth = TvRegLog.getText().toString().equals("Авторизация");
        updateForm(!currentIsAuth);
    }

    // Обновляем интерфейс в зависимости от выбранного режима
    private void updateForm(boolean isAuthMode) {
        if (isAuthMode) {
            TvRegLog.setText("Авторизация");
            btnRegOrLog.setText("Перейти к регистрации");
            btnAutorizz.setText("Войти");
            EDEmail.setVisibility(View.GONE); // Почту прячем при авторизации
        } else {
            TvRegLog.setText("Регистрация");
            btnRegOrLog.setText("Перейти к авторизации");
            btnAutorizz.setText("Зарегистрироваться");
            EDEmail.setVisibility(View.VISIBLE); // Почту показываем при регистрации
        }
    }

    // Обработка отправки формы (авторизация или регистрация)
    private void submitForm() {
        String username = EDLog.getText().toString();
        String password = EDPass.getText().toString();
        String email = "";

        if (TvRegLog.getText().toString().equals("Регистрация")) {
            email = EDEmail.getText().toString(); // Забираем почту при регистрации
        }

        if (TvRegLog.getText().toString().equals("Авторизация")) {
            new AuthenticateUserTask().execute(username, password); // Проверка пользователя
        } else {
            new RegisterNewUserTask().execute(username, password, email); // Регистрация нового пользователя
        }
    }

    // Класс для асинхронной проверки пользователя
    private class AuthenticateUserTask extends AsyncTask<String, Void, Integer> {
        @Override
        protected Integer doInBackground(String... strings) {
            String username = strings[0];
            String password = strings[1];

            try {
                Class.forName("net.sourceforge.jtds.jdbc.Driver");
                Connection conn = DriverManager.getConnection(CONNECTION_STRING);

                // Проверяем существование пользователя с данным именем и паролем
                String sql = "SELECT UserID FROM Users WHERE Username=? AND Password=?";
                PreparedStatement stmt = conn.prepareStatement(sql);
                stmt.setString(1, username);
                stmt.setString(2, password);

                ResultSet rs = stmt.executeQuery();

                if (rs.next()) {
                    return rs.getInt("UserID"); // Возвратим UserID
                }

            } catch (ClassNotFoundException | SQLException e) {
                Log.e(TAG, "Ошибка при проверке пользователя.", e);
            }

            return -1; // Неудача при поиске пользователя
        }

        @Override
        protected void onPostExecute(Integer userID) {
            if (userID != -1) {
                // Переход на SecondActivity с передачей UserID
                Intent intent = new Intent(MainActivity.this, SecondActivity.class);
                intent.putExtra("user_id", userID);
                startActivity(intent);
            } else {
                Toast.makeText(MainActivity.this, "Ошибка авторизации! Проверьте имя пользователя и пароль.", Toast.LENGTH_LONG).show();
            }
        }
    }

    // Класс для асинхронной регистрации нового пользователя
    private class RegisterNewUserTask extends AsyncTask<String, Void, Boolean> {
        @Override
        protected Boolean doInBackground(String... strings) {
            String username = strings[0];
            String password = strings[1];
            String email = strings[2];

            try {
                Class.forName("net.sourceforge.jtds.jdbc.Driver");
                Connection conn = DriverManager.getConnection(CONNECTION_STRING);

                PreparedStatement stmt = conn.prepareStatement(
                        "INSERT INTO Users (Username, Password, Email, RoleID) VALUES (?, ?, ?, 2)"
                );
                stmt.setString(1, username);
                stmt.setString(2, password);
                stmt.setString(3, email);

                int rowsInserted = stmt.executeUpdate();
                return rowsInserted > 0; // Результат успешной регистрации
            } catch (ClassNotFoundException | SQLException e) {
                Log.e(TAG, "Ошибка при регистрации пользователя.", e);
                return false;
            }
        }

        @Override
        protected void onPostExecute(Boolean result) {
            if (result) {
                Toast.makeText(MainActivity.this, "Вы успешно зарегистрированы!", Toast.LENGTH_SHORT).show();
            } else {
                Toast.makeText(MainActivity.this, "Ошибка регистрации! Попробуйте снова.", Toast.LENGTH_SHORT).show();
            }
        }
    }
}

