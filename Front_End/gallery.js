function displayBooks(books) {
    const gallery = document.getElementById('gallery');
    gallery.innerHTML = '';
    books.forEach(book => {
        const bookCard = document.createElement('div');
        bookCard.classList.add('book-card');
        bookCard.innerHTML = `
            <img src="${book.coverUrl}" alt="${book.name}">
            <h3>${book.name}</h3>
            <p>ISBN: ${book.isbn}</p>
            <p>Publisher: ${book.publisher}</p>
            <p>Genre: ${book.genre}</p>
            <p>Copies: ${book.copies}</p>
            <button onclick="requestBook('${book.name}')">Request</button>
        `;
        gallery.appendChild(bookCard);
    });
}

function requestBook(bookName) {
    const currentMember = JSON.parse(localStorage.getItem('loggedInMember'));
    if (!currentMember) {
        alert('You must be logged in to request a book.');
        return;
    }

    let requests = JSON.parse(localStorage.getItem('requests')) || [];
    requests.push({
        nic: currentMember.nic,
        bookName: bookName,
        memberName: currentMember.firstName,
    });
    localStorage.setItem('requests', JSON.stringify(requests));
    alert('Book requested successfully!');
}

function sortBooks(criteria) {
    let books = JSON.parse(localStorage.getItem('books')) || [];
    books.sort((a, b) => a[criteria].localeCompare(b[criteria]));
    displayBooks(books);
}

function searchBooks(query) {
    let books = JSON.parse(localStorage.getItem('books')) || [];
    let filteredBooks = books.filter(book => 
        book.name.toLowerCase().includes(query.toLowerCase()) ||
        book.author.toLowerCase().includes(query.toLowerCase()) ||
        book.genre.toLowerCase().includes(query.toLowerCase())
    );
    displayBooks(filteredBooks);
}

document.getElementById('sortOptions').addEventListener('change', function() {
    sortBooks(this.value);
});

document.getElementById('searchInput').addEventListener('input', function() {
    searchBooks(this.value);
});

window.onload = function() {
    let books = JSON.parse(localStorage.getItem('books')) || [];
    displayBooks(books);
};