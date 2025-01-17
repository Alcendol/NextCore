"use client";

import Link from "next/link"
import Search from "@/components/search"
import { useEffect, useState } from "react"
import { IoAlertCircleSharp, IoAddCircle, } from "react-icons/io5";


interface Author {
    authorId: string;
    authorName: string;
    authorEmail: string;
    authorPhone: string;
}

const AuthorPage: React.FC = () => {
    const [authors, setAuthors] = useState<Author[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const itemsPerPage = 20;

    useEffect(() => {
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/author`)
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
            setAuthors(data);
            setLoading(false);
          })
          .catch((error) => {
            console.error('Error fetching data:', error);
            setError('Failed to fetch data. Please try again later.');
            setLoading(false);
          });
        }, 
    []);

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentBooks = authors.slice(indexOfFirstItem, indexOfLastItem);
  
    const totalPages = Math.ceil(authors.length / itemsPerPage);
  
    const handlePageChange = (page: number) => {
      setCurrentPage(page);
    };

    return (
        <div>
            <div className='p-4 mt-20'>
                <div className="w-full flex justify-end">
                    <Link href="/admindashboard/book/create">
                        <button className="bg-blue-600 h-14 rounded-lg hover:bg-blue-800">
                            <span className="w-full text-white p-5">Create New Data</span>
                        </button>
                    </Link>
                </div>
            </div>
            <div className="p-4">
                <Search  />
            </div>
            <div className="w-full px-4 py-2">
                <div className="flex justify-start">
                    <span className="font-sans text-2xl">Data Author yang sudah di input</span>
                </div>
            </div>
            {loading && <p>Loading...</p>}
            {error && <p style={{ color: "red" }}>{error}</p>}
            {!loading && authors.length === 0 && <p>No books available.</p>}
            <div className="w-full p-4">
                <table className="table-auto w-full border-collapse border border-gray-200 shadow-lg">
                    <thead>
                        <tr className="bg-blue-500 text-white text-center">
                            <th className="px-4 py-2 border border-gray-300 rounded-tl-lg">Id Author</th>
                            <th className="px-4 py-2 border border-gray-300">Nama Author</th>
                            <th className="hidden md:table-cell px-4 py-2 border border-gray-300">Email Author</th>
                            <th className="hidden md:table-cell px-4 py-2 border border-gray-300">Nomor Handphone</th>
                            <th className="px-4 py-2 border border-gray-300 rounded-tr-lg">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {currentBooks.map((author) => (
                            <tr key={author.authorId} className="bg-gray-100 hover:bg-gray-200">
                                <td className="px-4 py-2 border border-gray-300">{author.authorId}</td>
                                <td className="px-4 py-2 border border-gray-300">{author.authorName}</td>
                                <td className="hidden md:table-cell px-4 py-2 border border-gray-300">{author.authorEmail}</td>
                                <td className="hidden md:table-cell px-4 py-2 border border-gray-300">{author.authorPhone}</td>
                                <td className="px-4 py-2 border border-gray-300 flex justify-center">
                                    <Link href={`/admindashboard/author/${author.authorId}`}>
                                    <button className="bg-blue-600 h-14 rounded-lg hover:bg-blue-800">
                                        <span className="w-full text-white p-5">View</span>
                                    </button>
                                    </Link>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
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

export default AuthorPage