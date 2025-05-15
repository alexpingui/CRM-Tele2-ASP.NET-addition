const phoneInput = document.getElementById('phoneInput');
const suggestions = document.getElementById('suggestions');

if (window.clientFromServer) {
    window.addEventListener('DOMContentLoaded', () => {
        const client = window.clientFromServer;
        if (client) {
            loadInformation(client);
        }
    });
}


phoneInput.addEventListener('input', async function () {
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
    const callHistory = document.getElementById('callHistoryTable');
    const response = await fetch(`/OutGoingCall/LoadCallHistory?phoneNumber=${encodeURIComponent(phoneNumber)}`);
    const calls = await response.json();

    const tbody = callHistory.querySelector('tbody');

    while (tbody.firstChild) {
        tbody.removeChild(tbody.firstChild);
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

        tbody.appendChild(item);
    });
}
