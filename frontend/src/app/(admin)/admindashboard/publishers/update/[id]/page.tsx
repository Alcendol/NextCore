"use client";

import Link from "next/link";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";

interface Publisher {
    publisherId: string;
    publisherName: string;
    publisherEmail: string;
    publisherPhone: string;
}

const UpdatePagePublisher: React.FC = () => {
    const [publisher, setPublisher] = useState<Publisher | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string | null>(null);
    const [id, setId] = useState<string | null>(null);
    const [isUpdating, setIsUpdating] = useState<boolean>(false);
    const [showPopup, setShowPopup] = useState<boolean>(false);

    const router = useRouter();

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target;
        if (publisher) {
            setPublisher({ ...publisher, [id]: value });
        }
    };

    useEffect(() => {
        const pathname = window.location.pathname;
        const idFromPath = pathname.split("/").pop();
        setId(idFromPath || null);
    }, []);

    useEffect(() => {
        if (id) {
            fetch(`${process.env.NEXT_PUBLIC_API_URL}/publisher/by-publisherid/${id}`)
                .then((response) => {
                    if (!response.ok) {
                        throw new Error("Failed to fetch publisher details.");
                    }
                    return response.json();
                })
                .then((data) => {
                    setPublisher(data);
                    setLoading(false);
                })
                .catch((error) => {
                    console.error(error);
                    setError("Failed to load publisher details.");
                    setLoading(false);
                });
        }
    }, [id]);

    const handleUpdate = async () => {
        if (!publisher) return;

        setIsUpdating(true);
        setError(null);

        try {
            const formData = new FormData();
            formData.append("publisherId", publisher.publisherId);
            formData.append("publisherName", publisher.publisherName);
            formData.append("publisherEmail", publisher.publisherEmail);
            formData.append("publisherPhone", publisher.publisherPhone);

            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/publisher/update/${publisher.publisherId}`, {
                method: "PUT",
                body: formData,
            });

            if (!res.ok) {
                const errorData = await res.json();
                console.error("Backend error:", errorData);
                throw new Error(errorData?.message || "Failed to update publisher.");
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
        router.push("/admindashboard/publishers");
    };

    if (loading) return <p>Loading...</p>;
    if (error) return <p style={{ color: "red" }}>{error}</p>;

    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            {showPopup && (
                <div className="fixed inset-0 flex items-center justify-center z-50 bg-black bg-opacity-30">
                    <div className="bg-white rounded-lg shadow-lg p-6 w-96 text-center">
                        <h2 className="text-lg font-semibold text-gray-700 mb-2">Update Successful!</h2>
                        <p className="text-sm text-gray-500">The publisher has been updated successfully.</p>
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
                <Link href={"/admindashboard/publisher"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Publisher
                </Link>
            </div>
            {publisher && (
                <div className="w-full flex justify-start items-center mb-5">
                    <span className="font-sans font-bold text-xl">Update Data Publisher {publisher.publisherName}</span>
                </div>
            )}
            {publisher && (
                <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                    <div className="bg-white border border-gray-400 p-4 w-full">
                        <label htmlFor="publisherId" className="block text-sm text-gray-700 font-sans mb-2">
                            Publisher ID:
                        </label>
                        <input
                            id="publisherId"
                            type="text"
                            value={publisher.publisherId}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg bg-gray-100 cursor-not-allowed focus:outline-none"
                        />
                        <label htmlFor="publisherName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Nama Publisher:
                        </label>
                        <input
                            id="publisherName"
                            type="text"
                            value={publisher.publisherName || ""}
                            onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="publisherEmail" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Email Publisher:
                        </label>
                        <input
                            id="publisherEmail"
                            type="text"
                            value={publisher.publisherEmail}
                            onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="publisherPhone" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Nomor HP Publisher:
                        </label>
                        <input
                            id="publisherPhone"
                            type="text"
                            value={publisher.publisherPhone}
                            onChange={handleInputChange}
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <div className="flex justify-end">
                            <Link href="/admindashboard/publishers">
                                <button className="mt-6 w-32 mx-2 bg-gray-100 border-2 text-white p-2 rounded-lg hover:bg-gray-200">
                                    <span className="font-sans font-bold text-gray-600">Cancel</span>
                                </button>
                            </Link>
                            <button
                                onClick={handleUpdate}
                                disabled={isUpdating}
                                className="mt-6 w-32 bg-yellow-400 border-2 border-yellow-300 text-white p-2 rounded-lg hover:bg-yellow-500 disabled:bg-gray-300"
                            >
                                <span className="font-sans font-bold text-gray-600">
                                    {isUpdating ? "Updating..." : "Update"}
                                </span>
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default UpdatePagePublisher;
