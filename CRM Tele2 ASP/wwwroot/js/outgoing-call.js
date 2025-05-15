const phoneInput = document.getElementById('phoneInput');
const suggestions = document.getElementById('suggestions');
const callHistory = document.getElementById('callHistoryTable');
const callHistorytbody = callHistory.querySelector('tbody');

if (window.clientFromServer) {
    window.addEventListener('DOMContentLoaded', () => {
        const client = window.clientFromServer;
        if (client) {
            loadInformation(client);
        }
    });
}


phoneInput.addEventListener('input', async function () {
    while (callHistorytbody.firstChild) {
        callHistorytbody.removeChild(callHistorytbody.firstChild);
    }

    const query = this.value;

    if (query.length >= 5) {
        const response = await fetch(`/OutGoingCall/SearchClientsByNumber?numberPart=${encodeURIComponent(query)}`);
        const clients = await response.json();

        suggestions.innerHTML = '';

        clients.forEach(client => {
            const item = document.createElement('div');

            item.classList.add('list-group-item');
            item.textContent = client.phoneNumber;

            item.addEventListener('click', () => {
                loadInformation(client);
            });
            suggestions.appendChild(item);
        });

        if (query.length == 12 && clients.length == 0)
        {
            suggestions.innerHTML = '';
            confirm("Клиент с данным номером не найден. Он будет добавлен в базу автоматически");
        }

    }
        else suggestions.innerHTML = '';
})


    function loadInformation(client) {
        phoneInput.value = client.phoneNumber;
        document.getElementById('clientName').value = client.name;
        document.getElementById('clientAddress').value = client.address;
        suggestions.innerHTML = '';

        LoadCallHistory(client.phoneNumber);
    }

async function LoadCallHistory(phoneNumber) {
    const response = await fetch(`/OutGoingCall/LoadCallHistory?phoneNumber=${encodeURIComponent(phoneNumber)}`);
    const calls = await response.json();


    while (callHistorytbody.firstChild) {
        callHistorytbody.removeChild(callHistorytbody.firstChild);
    }

    calls.forEach(call => {
        const item = document.createElement('tr');

        item.className = "callCell";

        const commentCell = item.insertCell();
        const dateOfCallCell = item.insertCell();
        const dateOfScheduledCallCell = item.insertCell();

        commentCell.innerHTML = call.label;
        dateOfCallCell.innerHTML = call.dateOfCall;
        dateOfScheduledCallCell.innerHTML = call.dateOfScheduledCall;

        callHistorytbody.appendChild(item);
    });
}
