package com.example.myapplication;

import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class fragment_create_request extends Fragment {

    private static final String CONNECTION_STRING = "jdbc:jtds:sqlserver://192.168.31.180;databaseName=RepairServiceDB;user=nefor;password=27062006nik;";

    private EditText etFIO, etPhone, etFault, etDescription;
    private Button btnSubmit;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_create_request, container, false);

        etFIO = view.findViewById(R.id.etFIO);
        etPhone = view.findViewById(R.id.etPhone);
        etFault = view.findViewById(R.id.etFault);
        etDescription = view.findViewById(R.id.etDescription);
        btnSubmit = view.findViewById(R.id.btnCreate);

        btnSubmit.setOnClickListener(v -> createRequest());

        return view;
    }

    private void createRequest() {
        Bundle args = getArguments();
        if (args != null && args.containsKey("user_id")) {
            int userID = args.getInt("user_id");

            new Thread(() -> {
                try {
                    Class.forName("net.sourceforge.jtds.jdbc.Driver");
                    Connection conn = DriverManager.getConnection(CONNECTION_STRING);

                    // Добавляем клиента в таблицу Clients
                    String insertClientSQL = "INSERT INTO Clients (FullName, Phone, UserID) VALUES (?, ?, ?)";
                    PreparedStatement insertClientStmt = conn.prepareStatement(insertClientSQL, PreparedStatement.RETURN_GENERATED_KEYS);
                    insertClientStmt.setString(1, etFIO.getText().toString());
                    insertClientStmt.setString(2, etPhone.getText().toString());
                    insertClientStmt.setInt(3, userID);
                    insertClientStmt.executeUpdate();

                    // Получаем клиентский ID
                    ResultSet generatedKeys = insertClientStmt.getGeneratedKeys();
                    if (generatedKeys.next()) {
                        long clientID = generatedKeys.getLong(1);

                        // Создаем заявку
                        String insertRequestSQL = "INSERT INTO Requests (DateAdded, TypeOfFault, ProblemDescription, Status, ClientID)\n" +
                                "VALUES (GETDATE(), ?, ?, 'ожидает', ?)";
                        PreparedStatement insertRequestStmt = conn.prepareStatement(insertRequestSQL);
                        insertRequestStmt.setString(1, etFault.getText().toString());
                        insertRequestStmt.setString(2, etDescription.getText().toString());
                        insertRequestStmt.setLong(3, clientID);
                        insertRequestStmt.executeUpdate();

                        Handler mainHandler = new Handler(Looper.getMainLooper());
                        mainHandler.post(() -> {
                            Toast.makeText(requireContext(), "Заявка успешно создана!", Toast.LENGTH_SHORT).show();
                        });
                    } else {
                        showErrorMessage("Ошибка при сохранении заявки");
                    }

                } catch (SQLException | ClassNotFoundException e) {
                    e.printStackTrace();
                    showErrorMessage(e.getLocalizedMessage());
                }
            }).start();
        } else {
            showErrorMessage("Не указано значение user_id");
        }
    }

    private void showErrorMessage(String message) {
        Toast.makeText(requireContext(), message, Toast.LENGTH_SHORT).show();
    }
}