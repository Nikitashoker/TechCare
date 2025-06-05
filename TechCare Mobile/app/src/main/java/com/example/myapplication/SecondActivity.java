package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class SecondActivity extends AppCompatActivity {

    private Button btnShowRequests, btnCreateRequest;
    private int userID;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_second);

        // Получаем UserID из предыдущей активности
        userID = getIntent().getIntExtra("user_id", -1);

        btnShowRequests = findViewById(R.id.btnShowRequests);
        btnCreateRequest = findViewById(R.id.btnCreateRequest);

        // Открываем фрагмент по умолчанию (просмотр заявок)
        openFragment(new fragment_requests());

        btnShowRequests.setOnClickListener(v -> openFragment(new fragment_requests()));
        btnCreateRequest.setOnClickListener(v -> openFragment(new fragment_create_request()));
    }

    private void openFragment(Fragment fragment) {
        // Передаем UserID в аргументы фрагмента
        Bundle args = new Bundle();
        args.putInt("user_id", userID);
        fragment.setArguments(args);

        FragmentTransaction transaction = getSupportFragmentManager().beginTransaction();
        transaction.replace(R.id.fragment_container, fragment);
        transaction.addToBackStack(null);
        transaction.commit();
    }
}