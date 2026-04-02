var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7105/SignalRHub")
    .build();

document.getElementById("sendButton").disabled = true;
connection.on("ReceiveMessage", function (user, message) {
    var currentTime = new Date().toLocaleTimeString();

    var li = document.createElement("li");

    var span = document.createElement("span");
    span.style.fontWeight = "bold";
    span.textContent = user + ": ";

    li.appendChild(span);

    var text = document.createTextNode(`${message} - ${currentTime}`);
    li.appendChild(text);

    document.getElementById("messagesList").appendChild(li);
});

connection.start()
    .then(function () {
        document.getElementById("sendButton").disabled = false;
    })
    .catch(function (err) {
        console.error(err.toString());
    });

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userinput").value;
    var message = document.getElementById("messageinput").value;

    connection.invoke("SendMessage", user, message)
        .catch(function (err) {
            console.error(err.toString());
        });

    event.preventDefault();
});