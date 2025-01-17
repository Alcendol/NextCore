"use client";

import Search from "@/components/search"
import Card from "@/components/card"
import { useEffect, useState } from 'react'

interface Book {
    bookId: string;
    title: string;
    authorName: string;
    publisherName: string;
    datePublished: string;
    totalPage: number;
    country: string;
    language: string;
    genre: string;
    desc: string;
  }

interface Genre {
  genreId: string;
  genreName: string;
}

const PeminjamanBukuPage: React.FC = () => {
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const itemsPerPage = 10;

  useEffect(() => {
    fetch(`${process.env.NEXT_PUBLIC_API_URL}/book`)
      .then((response) => {
        if (!response.ok) {
          return response.text().then((text) => {
            throw new Error(`Network response was not ok. Status: ${response.status}, ${text}`);
          });
        }
        return response.json();
      })
      .then((data) => {
        console.log('Fetched data:', data);
        setBooks(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
        setError('Failed to fetch data. Please try again later.');
        setLoading(false);
      });
  }, []);

  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentBooks = books.slice(indexOfFirstItem, indexOfLastItem);

  const totalPages = Math.ceil(books.length / itemsPerPage);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };
  

  return (
    <div className="my-10">
        <div className="px-6">
          <Search />
        </div>
        {loading && <p>Loading...</p>}
        {error && <p style={{ color: "red" }}>{error}</p>}
        {!loading && books.length === 0 && <p>No books available.</p>}
        <div className="grid grid-cols-1 gap-4"> 
            {currentBooks.map((book) => (
              <Card
                key={book.bookId}
                bookId={book.bookId}
                title={book.title}
                desc={book.desc}
                genre={typeof book.genre === "string" ? book.genre.split(",") : book.genre}
                datePublished={book.datePublished}
                authorName={book.authorName}
              />
            ))}
        </div>
      {totalPages > 1 && (
      <div className="flex justify-center mt-6">
        <nav className="inline-flex -space-x-px">
          <button
                onClick={() => handlePageChange(currentPage - 1)}
                disabled={currentPage === 1}
                className={`px-4 py-2 rounded-l-lg border border-gray-300 bg-white text-sm font-medium ${
                  currentPage === 1 ? "text-gray-400 cursor-not-allowed" : "text-gray-700 hover:bg-gray-100"
                }`}
              >
                Previous
          </button>
          {Array.from({ length: totalPages }, (_, i) => i + 1).map((page) => (
              <button
                key={page}
                onClick={() => handlePageChange(page)}
                className={`px-4 py-2 border border-gray-300 bg-white text-sm font-medium ${
                  page === currentPage
                    ? "bg-blue-500 text-white"
                    : "text-gray-700 hover:bg-gray-100"
                }`}
              >
                {page}
              </button>
            ))}
            <button
              onClick={() => handlePageChange(currentPage + 1)}
              disabled={currentPage === totalPages}
              className={`px-4 py-2 rounded-r-lg border border-gray-300 bg-white text-sm font-medium ${
                currentPage === totalPages ? "text-gray-400 cursor-not-allowed" : "text-gray-700 hover:bg-gray-100"
              }`}
            >
              Next
            </button>
          </nav>
        </div>
    )}
    </div>
  )
}

export default PeminjamanBukuPage