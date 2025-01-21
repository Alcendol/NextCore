"use client";

import Link from "next/link"
import { useEffect, useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";


interface Publisher {
    publisherId: string;
    publisherName: string;
    publisherEmail: string;
    publisherPhone: string;
}

const ViewPagePublisher: React.FC = () => {
    const [publisher, setPublisher] = useState<Publisher | null>(null);
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
        }, 
    [id]);
    if (loading) return <p>Loading...</p>;
    if (error) return <p style={{ color: "red" }}>{error}</p>;

    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            <div className="mb-5">
                <Link href={"/admindashboard/publisher"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Publisher
                </Link>
            </div>
            {publisher && (
                <div className="w-full flex justify-start items-center mb-5">
                    <span className="font-sans font-bold text-xl">Detail Data Publisher {publisher.publisherName}</span>
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
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="publisherName" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Nama Publisher
                        </label>
                        <input
                            id="publisherName"
                            type="text"
                            value={publisher.publisherName}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="publisherEmail" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Email Publisher
                        </label>
                        <input
                            id="publisherEmail"
                            type="text"
                            value={publisher.publisherEmail}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                        <label htmlFor="publisherPhone" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                            Nomor Hp Publisher
                        </label>
                        <input
                            id="publisherPhone"
                            type="text"
                            value={publisher.publisherPhone}
                            disabled
                            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                        />
                    </div>
                </div>
            )}
        </div>
    );
};

export default ViewPagePublisher;