

var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/BookList")
    .build();



function RefreshList() {

    hubConnection.invoke("RefreshList");

}


hubConnection.start();

