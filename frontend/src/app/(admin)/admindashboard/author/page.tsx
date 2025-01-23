"use client";

import Link from "next/link"
import Search from "@/components/search"
import { useEffect, useState } from "react"
import { IoAlertCircleSharp, IoAddCircle, } from "react-icons/io5";


interface Author {
    authorId: string;
    firstName: string;
    lastName: string;
    authorEmail: string;
    authorPhone: string;
}

const AuthorPage: React.FC = () => {
    const [authors, setAuthors] = useState<Author[]>([]);
    const [author, setAuthor] = useState<Author | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [isDeleting, setIsDeleting] = useState<boolean>(false);
    const [showDeletePopup, setShowDeletePopup] = useState<boolean>(false);
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
    const currentAuthors = authors.slice(indexOfFirstItem, indexOfLastItem);
  
    const totalPages = Math.ceil(authors.length / itemsPerPage);
  
    const handlePageChange = (page: number) => {
      setCurrentPage(page);
    };

    // Handling Delete
    const handleDeleteClick = (authorId: string) => {
        setAuthor(authors.find((a) => a.authorId === authorId) || null);
        setShowDeletePopup(true);
    };
    
    const handleDeleteConfirm = async () => {
        if (!author) return;
    
        setIsDeleting(true);
        setError(null);
    
        try {
            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/author/delete/${author.authorId}`, {
                method: "POST",
            });
    
            if (!res.ok) {
                const errorData = await res.json();
                console.error("Backend error:", errorData);
                throw new Error(errorData?.message || "Failed to delete author.");
            }
    
            setAuthors(authors.filter((a) => a.authorId !== author.authorId));
            setShowDeletePopup(false);
        } catch (err) {
            console.error("Error deleting author:", err);
            setError((err as Error).message || "An unexpected error occurred.");
        } finally {
            setIsDeleting(false);
        }
    };
    
    const handleDeleteCancel = () => {
        setShowDeletePopup(false);
    };

    return (
        <div>
            <div className='p-4 mt-20'>
                <div className="w-full flex justify-end">
                    <Link href="/admindashboard/author/create">
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
                        {currentAuthors.map((author) => (
                            <tr key={author.authorId} className="bg-gray-100 hover:bg-gray-200">
                                <td className="px-4 py-2 border border-gray-300">{author.authorId}</td>
                                <td className="px-4 py-2 border border-gray-300">{author.firstName} {author.lastName}</td>
                                <td className="hidden md:table-cell px-4 py-2 border border-gray-300">{author.authorEmail}</td>
                                <td className="hidden md:table-cell px-4 py-2 border border-gray-300">{author.authorPhone}</td>
                                <td className="py-2 border border-gray-300">
                                    <div className="block text-center 2xl:flex justify-center gap-2">
                                        <Link href={`/admindashboard/author/view/${author.authorId}`}>
                                        <button className="bg-blue-600 w-20 m-2 py-2 rounded-lg hover:bg-blue-800">
                                            <span className="text-white text-sm">View</span>
                                        </button>
                                        </Link>
                                        <Link href={`/admindashboard/author/update/${author.authorId}`}>
                                        <button className="bg-yellow-400 w-20 m-2 py-2 rounded-lg hover:bg-yellow-500">
                                            <span className="text-white text-sm">Update</span>
                                        </button>
                                        </Link>
                                        <Link href={``}>
                                        <button 
                                            onClick={() => handleDeleteClick(author.authorId)}
                                            className="bg-red-600 w-20 py-2 m-2 rounded-lg hover:bg-red-700">
                                            <span className="text-white text-sm">Delete</span>
                                        </button>
                                        </Link>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>  

            {showDeletePopup && (
                <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-30">
                    <div className="bg-white rounded-lg shadow-lg p-6 w-96 text-center">
                        <h2 className="text-lg font-semibold text-gray-700 mb-2">Confirm Deletion</h2>
                        <p className="text-sm text-gray-500 mb-6">
                            Are you sure you want to delete this author? This action cannot be undone.
                        </p>
                        <div className="flex justify-around">
                            <button
                                onClick={handleDeleteCancel}
                                className="px-4 py-2 bg-gray-400 text-white rounded-lg hover:bg-gray-500"
                            >
                                Cancel
                            </button>
                            <button
                                onClick={handleDeleteConfirm}
                                disabled={isDeleting}
                                className="px-4 py-2 bg-red-600 text-white rounded-lg hover:bg-red-700 disabled:bg-gray-300"
                            >
                                {isDeleting ? "Deleting..." : "Confirm"}
                            </button>
                        </div>
                    </div>
                </div>
            )}

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