"use client";

import Link from "next/link";
import { useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";
import { useRouter } from "next/navigation";

const CreatePageGenre = () => {
    const [genreId, setGenreId] = useState("");
    const [genreName, setGenreName] = useState("");
    const [showConfirmation, setShowConfirmation] = useState(false);

    const router = useRouter();

    const handleSubmit = async () => {
        try {
            const formData = new FormData();
            formData.append("genreId", genreId);
            formData.append("genreName", genreName);

            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/genre/single`, {
                method: "POST",
                body: formData,
            });

            if (res.ok) {
                router.push("/admindashboard/genre");
            } else {
                const errorData = await res.json();
                console.error("Error Response: ", errorData);
                alert(`Error: ${errorData?.title || "Failed to Create New Genre"}`);
            }
        } catch (error) {
            console.error("Error during submission:", error);
        } finally {
            setShowConfirmation(false);
        }
    };

    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!genreId || !genreName) {
            alert("All fields are required");
            return;
        }
        setShowConfirmation(true);
    };

    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            <div className="mb-5">
                <Link href={"/admindashboard/genre"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Genre
                </Link>
            </div>
            <div className="w-full flex justify-start items-center mb-5">
                <span className="font-sans font-bold text-xl">Tambah Genre</span>
            </div>
            <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                <form
                    className="bg-white border border-gray-400 p-4 w-full"
                    onSubmit={handleFormSubmit}
                >
                    <label htmlFor="genreId" className="block text-sm text-gray-700 font-sans mb-2">
                        Id Genre:
                    </label>
                    <input
                        id="genreId"
                        type="text"
                        onChange={(e) => setGenreId(e.target.value)}
                        value={genreId}
                        placeholder="e.g: 1231"
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="genreName" className="block text-sm text-gray-700 font-sans mb-2">
                        Nama Genre:
                    </label>
                    <input
                        id="genreName"
                        type="text"
                        onChange={(e) => setGenreName(e.target.value)}
                        value={genreName}
                        placeholder="e.g: Fantasy, Drama,..."
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <div className="flex justify-end">
                        <Link href="/admindashboard/genre">
                            <button
                                type="button"
                                className="mt-6 w-32 mx-2 bg-gray-100 border-2 text-white p-2 rounded-lg hover:bg-gray-200 disabled:bg-gray-300"
                            >
                                <span className="font-sans font-bold text-gray-600">Cancel</span>
                            </button>
                        </Link>
                        <button
                            type="submit"
                            className="mt-6 w-32 bg-blue-500 text-white p-2 rounded-lg hover:bg-blue-600 disabled:bg-gray-300"
                        >
                            <span className="font-sans font-bold text-white">Tambah</span>
                        </button>
                    </div>
                </form>
            </div>

            {showConfirmation && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
                    <div className="bg-white p-6 rounded-lg shadow-lg max-w-sm w-full">
                        <h3 className="text-lg font-bold mb-4">Are you sure?</h3>
                        <p className="text-sm text-gray-600 mb-6">
                            Do you want to add this genre with the provided details?
                        </p>
                        <div className="flex justify-end">
                            <button
                                onClick={() => setShowConfirmation(false)}
                                className="bg-gray-200 text-gray-800 py-2 px-4 rounded-lg mr-2 hover:bg-gray-300"
                            >
                                Cancel
                            </button>
                            <button
                                onClick={handleSubmit}
                                className="bg-blue-500 text-white py-2 px-4 rounded-lg hover:bg-blue-600"
                            >
                                Confirm
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default CreatePageGenre;
