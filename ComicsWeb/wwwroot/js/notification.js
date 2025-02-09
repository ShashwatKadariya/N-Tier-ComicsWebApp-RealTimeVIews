const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notifications")
    .withAutomaticReconnect()
    .build()




connection.on("ViewCountUpdated", (comicId, newViewCount) => {
    const viewCountElement = document.getElementById("comicViewCount");
     if(viewCountElement) {
         viewCountElement.innerText = newViewCount;
     }
     const viewCountElementWithId = document.getElementById(`comicViewCount-${comicId}`);
     if(viewCountElementWithId) {
         viewCountElementWithId.innerText = newViewCount;
     }
});

connection.start().catch(error => console.log("Error in SignalR connection: ", error));
