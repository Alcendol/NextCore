"use client";

import Link from "next/link"
import { useEffect, useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";


interface Author {
    authorId: string;
    authorName: string;
    authorEmail: string;
    authorPhone: string;
}

const ViewPageAuthor: React.FC = () => {
    const [author, setAuthor] = useState<Author | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [id, setId] = useState<string | null>(null);

    useEffect(() => {
        const pathname = window.location.pathname;
        const idFromPath = pathname.split("/").pop();
        setId(idFromPath || null);
        }, []);
    
        useEffect(() => {
            if (id) {
            fetch(`${process.env.NEXT_PUBLIC_API_URL}/author/by-authorid/${id}`)
                .then((response) => {
                if (!response.ok) {
                    throw new Error("Failed to fetch author details.");
                }
                return response.json();
                })
                .then((data) => {
                setAuthor(data);
                setLoading(false);
                })
                .catch((error) => {
                console.error(error);
                setError("Failed to load author details.");
                setLoading(false);
                });
            }
        }, 
    [id]);
    if (loading) return <p>Loading...</p>;
    if (error) return <p style={{ color: "red" }}>{error}</p>;

    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            <div className="mb-5">
                <Link href={"/admindashboard/author"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Author
                </Link>
            </div>
            {author && (
                <div className="w-full flex justify-start items-center mb-5">
                    <span className="font-sans font-bold text-xl">Detail Data Author {author.authorName}</span>
                </div>
            )}
            {author && (
                <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                    <div className="bg-white border border-gray-400 p-4 w-full">
                        <label htmlFor="authorId" className="block text-sm text-gray-700 font-sans mb-2">
                            Author ID:
                        </label>
                        <input
                            id="authorId"
                            type="text"
                            value={author.authorId}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="authorName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Nama Author
                        </label>
                        <input
                            id="authorName"
                            type="text"
                            value={author.authorName}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="authorEmail" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Email Author:
                        </label>
                        <input
                            id="authorEmail"
                            type="text"
                            value={author.authorEmail}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="authorPhone" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Nomor Hp Author:
                        </label>
                        <input
                            id="authorPhone"
                            type="text"
                            value={author.authorPhone}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                    </div>
                </div>
            )}
        </div>
    );
};

export default ViewPageAuthor;