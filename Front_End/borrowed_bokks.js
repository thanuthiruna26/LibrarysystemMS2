function displayBorrowedBooks() {
    const borrowedBooksTable = document.querySelector('#borrowedBooksTable tbody');
    const borrowedBooksCards = document.getElementById('borrowedBooksCards');
    let borrowedBooks = JSON.parse(localStorage.getItem('borrowedBooks')) || [];
    const currentMember = JSON.parse(localStorage.getItem('loggedInMember'));
    let books = JSON.parse(localStorage.getItem('books')) || [];

    borrowedBooksTable.innerHTML = '';
    borrowedBooksCards.innerHTML = '';

    borrowedBooks
        .filter(b => b.nic === currentMember.nic)
        .forEach(b => {
            // Find the corresponding book details
            const bookDetails = books.find(book => book.name === b.bookName);
            const currentDate = new Date().toLocaleDateString();

            // Add to table
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${b.bookName}</td>
                <td>${bookDetails.isbn}</td>
                <td>${bookDetails.publisher}</td>
                <td>${currentDate}</td>
            `;
            borrowedBooksTable.appendChild(row);

            // Add to cards
            const bookCard = document.createElement('div');
            bookCard.className = 'card';
            bookCard.innerHTML = `
                <img src="${bookDetails.coverUrl}" alt="${b.bookName}">
                <h3>${b.bookName}</h3>
                <p>ISBN: ${bookDetails.isbn}</p>
                <p>Publisher: ${bookDetails.publisher}</p>
                <p>Date Borrowed: ${currentDate}</p>
            `;
            borrowedBooksCards.appendChild(bookCard);
        });
}

window.onload = displayBorrowedBooks;