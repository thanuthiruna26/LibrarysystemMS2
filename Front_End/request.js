function displayRequests() {
    const requestTable = document.querySelector('#requestTable tbody');
    let requests = JSON.parse(localStorage.getItem('requests')) || [];

    requestTable.innerHTML = '';
    requests.forEach((request, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${request.nic}</td>
            <td>${request.memberName}</td>
            <td>${request.bookName}</td>
            <td><button onclick="acceptRequest(${index})">Accept</button></td>
        `;
        requestTable.appendChild(row);
    });
}

function acceptRequest(index) {
    let requests = JSON.parse(localStorage.getItem('requests')) || [];
    let borrowedBooks = JSON.parse(localStorage.getItem('borrowedBooks')) || [];
    const selectedRequest = requests[index];

    const memberBorrowedBooks = borrowedBooks.filter(b => b.nic === selectedRequest.nic);
    if (memberBorrowedBooks.length >= 2) {
        alert("Member cannot borrow more than 2 books at once.");
        return;
    }

    if (memberBorrowedBooks.some(b => b.bookName === selectedRequest.bookName)) {
        alert("Member cannot borrow the same book twice.");
        return;
    }

    borrowedBooks.push({
        nic: selectedRequest.nic,
        bookName: selectedRequest.bookName
    });

    localStorage.setItem('borrowedBooks', JSON.stringify(borrowedBooks));

    requests.splice(index, 1);
    localStorage.setItem('requests', JSON.stringify(requests));

    let books = JSON.parse(localStorage.getItem('books')) || [];
    books = books.map(book => {
        if (book.name === selectedRequest.bookName) {
            book.copies--;
        }
        return book;
    });
    localStorage.setItem('books', JSON.stringify(books));

    displayRequests();
}

window.onload = displayRequests;