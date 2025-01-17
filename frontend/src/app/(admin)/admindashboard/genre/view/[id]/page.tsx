"use client";

import Link from "next/link"
import { useEffect, useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";


interface Genre {
    genreId: string;
    genreName: string;
}

const ViewPageGenre: React.FC = () => {
    const [genre, setGenre] = useState<Genre | null>(null);
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
            fetch(`${process.env.NEXT_PUBLIC_API_URL}/genre/by-genreid/${id}`)
                .then((response) => {
                if (!response.ok) {
                    throw new Error("Failed to fetch genre details.");
                }
                return response.json();
                })
                .then((data) => {
                setGenre(data);
                setLoading(false);
                })
                .catch((error) => {
                console.error(error);
                setError("Failed to load genre details.");
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
                <Link href={"/admindashboard/genre"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Genre
                </Link>
            </div>
            {genre && (
                <div className="w-full flex justify-start items-center mb-5">
                    <span className="font-sans font-bold text-xl">Detail Data Genre {genre.genreName}</span>
                </div>
            )}
            {genre && (
                <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                    <div className="bg-white border border-gray-400 p-4 w-full">
                        <label htmlFor="genreId" className="block text-sm text-gray-700 font-sans mb-2">
                            Genre ID:
                        </label>
                        <input
                            id="genreId"
                            type="text"
                            value={genre.genreId}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="genreName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Nama Genre
                        </label>
                        <input
                            id="genreName"
                            type="text"
                            value={genre.genreName}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                    </div>
                </div>
            )}
        </div>
    );
};

export default ViewPageGenre;