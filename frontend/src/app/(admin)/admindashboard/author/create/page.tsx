"use client";

import Link from "next/link";
import { useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";
import { useRouter } from "next/navigation";

const CreatePageAuthor = () => {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [authorEmail, setAuthorEmail] = useState("");
    const [authorPhone, setAuthorPhone] = useState("");
    const [showConfirmation, setShowConfirmation] = useState(false);

    const router = useRouter();

    const handleSubmit = async () => {
        try {
            const formData = new FormData();
            formData.append("firstName", firstName);
            formData.append("lastName", lastName);
            formData.append("authorEmail", authorEmail);
            formData.append("authorPhone", authorPhone);

            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/author/single`, {
                method: "POST",
                body: formData,
            });

            if (res.ok) {
                router.push("/admindashboard/author");
            } else {
                const errorData = await res.json();
                console.error("Error Response: ", errorData);
                alert(`Error: ${errorData?.title || "Failed to Create New Author"}`);
            }
        } catch (error) {
            console.error("Error during submission:", error);
        } finally {
            setShowConfirmation(false);
        }
    };

    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!firstName || !lastName || !authorEmail || !authorPhone) {
            alert("All fields are required");
            return;
        }
        setShowConfirmation(true);
    };

    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            <div className="mb-5">
                <Link href={"/admindashboard/author"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Author
                </Link>
            </div>
            <div className="w-full flex justify-start items-center mb-5">
                <span className="font-sans font-bold text-xl">Tambah Author</span>
            </div>
            <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                <form
                    className="bg-white border border-gray-400 p-4 w-full"
                    onSubmit={handleFormSubmit}
                >
                    <label htmlFor="firstName" className="block text-sm text-gray-700 font-sans mb-2">
                        First Name:
                    </label>
                    <input
                        id="firstName"
                        type="text"
                        onChange={(e) => setFirstName(e.target.value)}
                        value={firstName}
                        placeholder="e.g: Tere Liye"
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="lastName" className="block text-sm text-gray-700 font-sans mb-2">
                        Last Name:
                    </label>
                    <input
                        id="lastName"
                        type="text"
                        onChange={(e) => setLastName(e.target.value)}
                        value={lastName}
                        placeholder="e.g: Tere Liye"
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="authorEmail" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Email Author:
                    </label>
                    <input
                        id="authorEmail"
                        type="text"
                        onChange={(e) => setAuthorEmail(e.target.value)}
                        value={authorEmail}
                        placeholder="e.g: xxxx@xxx.xx"
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="authorPhone" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Nomor Hp Author:
                    </label>
                    <input
                        id="authorPhone"
                        type="text"
                        onChange={(e) => setAuthorPhone(e.target.value)}
                        value={authorPhone}
                        placeholder="e.g: 08..."
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <div className="flex justify-end">
                        <Link href="/admindashboard/author">
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
                            Do you want to add this author with the provided details?
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

export default CreatePageAuthor;
