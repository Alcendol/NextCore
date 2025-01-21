"use client";

import Link from "next/link"
import { useEffect, useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";
import { useRouter } from "next/navigation";


interface Genre {
    genreId: string;
    genreName: string;
}

const UpdatePageGenre: React.FC = () => {
    const [genre, setGenre] = useState<Genre | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [id, setId] = useState<string | null>(null);
    const [isUpdating, setIsUpdating] = useState<boolean>(false);
    const [showPopup, setShowPopup] = useState<boolean>(false);

    const router = useRouter();

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target;
        if (genre) {
            setGenre({ ...genre, [id]: value });
        }
    };

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

    const handleUpdate = async () => {
        if (!genre) return;

        setIsUpdating(true);
        setError(null);

        try {
            const formData = new FormData();
            formData.append("genreId", genre.genreId);
            formData.append("genreName", genre.genreName);

            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/genre/update/${genre.genreId}`, {
                method: "PUT",
                body: formData,
            });

            if (!res.ok) {
                const errorData = await res.json();
                console.error("Backend error:", errorData);
                throw new Error(errorData?.message || "Failed to update genre.");
            }

            setShowPopup(true);
        } catch (err) {
            console.error("Error in update:", err);
            setError((err as Error).message || "An unexpected error occurred.");
        } finally {
            setIsUpdating(false);
        }
    };

    const handlePopupClose = () => {
        setShowPopup(false);
        router.push("/admindashboard/genre");
    };

    if (loading) return <p>Loading...</p>;
    if (error) return <p style={{ color: "red" }}>{error}</p>;

    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            {showPopup && (
                <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-30">
                    <div className="bg-white rounded-lg shadow-lg p-6 w-96 text-center">
                        <h2 className="text-lg font-semibold text-gray-700 mb-2">Update Successful!</h2>
                        <p className="text-sm text-gray-500">The genre has been updated successfully.</p>
                        <div className="mt-6">
                            <button
                                onClick={handlePopupClose}
                                className="p-3 px-8 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition duration-300"
                            >
                                Ok
                            </button>
                        </div>
                    </div>
                </div>
            )}

            <div className="mb-5">
                <Link href={"/admindashboard/genre"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Genre
                </Link>
            </div>
            {genre && (
                <div className="w-full flex justify-start items-center mb-5">
                    <span className="font-sans font-bold text-xl">Update Data Genre {genre.genreName}</span>
                </div>
            )}
            {genre && (
                <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                    <div className="bg-white border border-gray-400 p-4 w-full">
                        <label htmlFor="genreId" className="block text-sm text-gray-700 font-sans mb-2">
                            genre ID:
                        </label>
                        <input
                            id="genreId"
                            type="text"
                            defaultValue={genre.genreId}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="genreName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Nama genre:
                        </label>
                        <input
                            id="genreName"
                            type="text"
                            defaultValue={genre.genreName}
                            onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <div className="flex justify-end">
                            <Link href="/admindashboard/genre">
                                <button
                                    className="mt-6 w-32 mx-2 bg-gray-100 border-2 text-white p-2 rounded-lg hover:bg-gray-200 disabled:bg-gray-300">
                                    <span className="font-sans font-bold text-gray-600">
                                        Cancel
                                    </span>
                                </button>
                            </Link>
                            <button
                                onClick={handleUpdate}
                                disabled={isUpdating}
                                className="mt-6 w-32 bg-yellow-400 border-2 border-yellow-300 text-white p-2 rounded-lg hover:bg-yellow-500 disabled:bg-gray-300">
                                <span className="font-sans font-bold text-gray-600">
                                    {isUpdating ? "Updating..." : "Update Genre"}
                                </span>
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default UpdatePageGenre;