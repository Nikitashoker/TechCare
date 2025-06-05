package com.example.myapplication;

import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class fragment_requests extends Fragment {

    private static final String CONNECTION_STRING = "jdbc:jtds:sqlserver://192.168.31.180;databaseName=RepairServiceDB;user=nefor;password=27062006nik;";

    private RecyclerView rvRequests;
    private RequestAdapter adapter;
    private List<RequestItem> requests;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_requests, container, false);

        rvRequests = view.findViewById(R.id.recycler_view_requests);
        rvRequests.setLayoutManager(new LinearLayoutManager(requireContext()));
        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(rvRequests.getContext(),
                DividerItemDecoration.VERTICAL);
        rvRequests.addItemDecoration(dividerItemDecoration);

        loadDataFromDatabase();

        return view;
    }

    private void loadDataFromDatabase() {
        Bundle args = getArguments();
        if (args != null && args.containsKey("user_id")) {
            int userID = args.getInt("user_id");

            new Thread(() -> {
                try {
                    Class.forName("net.sourceforge.jtds.jdbc.Driver");
                    Connection conn = DriverManager.getConnection(CONNECTION_STRING);

                    // Выборка заявок по данному UserID
                    String sql = "SELECT r.RequestID, r.DateAdded, r.TypeOfFault, r.ProblemDescription, r.Status\n" +
                            "FROM Requests r\n" +
                            "INNER JOIN Clients c ON r.ClientID = c.ClientID\n" +
                            "WHERE c.UserID = ?\n" +
                            "ORDER BY r.DateAdded DESC";

                    PreparedStatement pstmt = conn.prepareStatement(sql);
                    pstmt.setInt(1, userID);

                    ResultSet rs = pstmt.executeQuery();

                    requests = new ArrayList<>();

                    while (rs.next()) {
                        int requestID = rs.getInt("RequestID");
                        String dateAdded = rs.getString("DateAdded");
                        String typeOfFault = rs.getString("TypeOfFault");
                        String problemDesc = rs.getString("ProblemDescription");
                        String status = rs.getString("Status");

                        requests.add(new RequestItem(requestID, dateAdded, typeOfFault, problemDesc, status));
                    }

                    Handler mainHandler = new Handler(Looper.getMainLooper());
                    mainHandler.post(() -> {
                        adapter = new RequestAdapter(requests);
                        rvRequests.setAdapter(adapter);
                    });

                } catch (SQLException | ClassNotFoundException e) {
                    e.printStackTrace();
                    showErrorMessage(e.getLocalizedMessage());
                }
            }).start();
        } else {
            showErrorMessage("Не указан идентификатор пользователя!");
        }
    }

    private void showErrorMessage(String message) {
        Toast.makeText(requireContext(), message, Toast.LENGTH_SHORT).show();
    }

    // Вспомогательная сущность для хранения одной заявки
    static class RequestItem {
        int id;
        String date;
        String fault;
        String description;
        String status;

        RequestItem(int id, String date, String fault, String description, String status) {
            this.id = id;
            this.date = date;
            this.fault = fault;
            this.description = description;
            this.status = status;
        }
    }

    // Адаптер для отображения заявок
    static class RequestAdapter extends RecyclerView.Adapter<RequestAdapter.RequestHolder> {
        private final List<RequestItem> items;

        RequestAdapter(List<RequestItem> items) {
            this.items = items;
        }

        @NonNull
        @Override
        public RequestHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
            View itemView = LayoutInflater.from(parent.getContext())
                    .inflate(android.R.layout.simple_list_item_2, parent, false);
            return new RequestHolder(itemView);
        }

        @Override
        public void onBindViewHolder(@NonNull RequestHolder holder, int position) {
            RequestItem item = items.get(position);
            holder.bind(item);
        }

        @Override
        public int getItemCount() {
            return items.size();
        }

        static class RequestHolder extends RecyclerView.ViewHolder {
            RequestHolder(View itemView) {
                super(itemView);
            }

            void bind(RequestItem item) {
                ((TextView) itemView.findViewById(android.R.id.text1))
                        .setText("Запись №" + item.id + ": " + item.date);
                ((TextView) itemView.findViewById(android.R.id.text2))
                        .setText("Тип неисправности: " + item.fault + "\nОписание: " + item.description + "\nСтатус: " + item.status);
            }
        }
    }
}