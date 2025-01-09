"use client"
import Search from "@/components/search"
import Card from "@/components/card"
import { useEffect, useState } from 'react'

interface Book {
    bookId: string;
    title: string;
    datePublished: string;
    totalPage: number;
    country: string;
    language: string;
    genre: string;
    desc: string;
  }

const PeminjamanBukuPage: React.FC = () => {
      const [books, setBooks] = useState<Book[]>([]);
      const [loading, setLoading] = useState<boolean>(true); // To track loading state
      const [error, setError] = useState<string | null>(null); // To track error state
    
      useEffect(() => {
        fetch('http://localhost:5259/api/BookController')
          .then((response) => {
            if (!response.ok) {
              console.error('Error fetching data:', response.statusText);
              throw new Error('Network response was not ok');
            }
            return response.json();
          })
          .then((data) => {
            console.log('Fetched data:', data); // Log the data to inspect it
            setBooks(data);
            setLoading(false);
          })
          .catch((error) => {
            console.error('Error fetching data:', error);
            setError('Failed to fetch data. Please try again later.');
            setLoading(false);
          });
      }, []);

    return (
        <div className="my-10">
            <Search />
            {loading && <p>Loading...</p>}
            {error && <p style={{ color: "red" }}>{error}</p>}
            {!loading && books.length === 0 && <p>No books available.</p>}
            {books.map((book) => (
                <Card
                    key={book.bookId}
                    title={book.title}
                    desc={book.desc}
                    genre={book.genre}
                    datePublished={book.datePublished}
                />
            ))}
        </div>
    )
}

export default PeminjamanBukuPage