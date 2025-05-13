async function LoadScheduledCalls() {
    const response = await fetch(`/Home/GetScheduledCalls`);
    const calls = await response.json();
    console.log(calls);
    const table = document.getElementById('scheduledCallsTable');
    const tbody = table.querySelector('tbody');


    calls.forEach(call => {
        const item = document.createElement('tr');

        item.addEventListener('click', async function() {
            const phone = call.clientPhoneNumber;
            window.location.href = `/OutGoingCall/OutGoingCallIndex?phone=${encodeURIComponent(phone)}`;
        })

        const nameCell = item.insertCell();      
        const addressCell = item.insertCell();
        const phoneNumberCell = item.insertCell();
        const commentCell = item.insertCell();
        const dateOfCallCell = item.insertCell();
        const dateOfScheduledCallCell = item.insertCell();

        nameCell.innerHTML = call.clientName;
        addressCell.innerHTML = call.clientAddress;
        phoneNumberCell.innerHTML = call.clientPhoneNumber;
        commentCell.innerHTML = call.comment;
        dateOfCallCell.innerHTML = call.dateOfCall;
        dateOfScheduledCallCell.innerHTML = call.dateOfScheduledCall;

        tbody.appendChild(item);
    })
}
document.addEventListener('DOMContentLoaded', () => {
    LoadScheduledCalls();
});