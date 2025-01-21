"use client";

import Link from "next/link";
import { useState } from "react";
import { IoCaretBackOutline } from "react-icons/io5";
import { useRouter } from "next/navigation";

const CreatePagePublisher = () => {
    const [publisherId, setPublisherId] = useState("");
    const [publisherName, setPublisherName] = useState("");
    const [publisherEmail, setPublisherEmail] = useState("");
    const [publisherPhone, setPublisherPhone] = useState("");
    const [showConfirmation, setShowConfirmation] = useState(false);
    const [emailError, setEmailError] = useState("");
    const [namaError, setNamaError] = useState("");
    const [phoneError, setPhoneError] = useState("");

    const router = useRouter();

    const handleSubmit = async () => {
        try {
            const formData = new FormData();
            formData.append("publisherId", publisherId);
            formData.append("publisherName", publisherName);
            formData.append("publisherEmail", publisherEmail);
            formData.append("publisherPhone", publisherPhone);

            const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/publisher/single`, {
                method: "POST",
                body: formData,
            });

            if (res.ok) {
                router.push("/admindashboard/publishers");
            } else {
                const errorData = await res.json();
                console.error("Error Response: ", errorData);
                alert(`Error: ${errorData?.title || "Failed to Create New Publisher"}`);
            }
        } catch (error) {
            console.error("Error during submission:", error);
        } finally {
            setShowConfirmation(false);
        }
    };

    const validateEmail = (email: string) => {
        const emailRegex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        
        if (!emailRegex.test(email)) {
            return "Please enter a valid email address";
        } else if (email.length < 5 || email.length > 50) {
            return "Email must be between 5 and 50 characters long";
        }
        return "";
    };

    const validateNama = (nama: string) => {
        if (nama.length < 3) {
            return "Nama harus diisi minimal 3 karakter";
        } else if (nama.length > 50) {
            return "Nama harus diisi maksimal 50 karakter";
        } else if (nama.trim() === "") {
            return "Nama tidak boleh kosong";
        }
        return "";
    };

    const validateNomorHp = (nomorHp: string) => {
        if (nomorHp.length < 10 || nomorHp.length > 14) {
            return "Nomor Hp harus diisi antara 10 - 14 karakter";
        } else if (nomorHp.trim() === "") {
            return "Nomor Hp tidak boleh kosong";
        }
        return "";
    };

    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        const emailValidationError = validateEmail(publisherEmail);
        const namaValidationError = validateNama(publisherName);
        const nomorHpValidationError = validateNomorHp(publisherPhone);

        setEmailError("");
        setNamaError("");
        setPhoneError("");

        if (emailValidationError) {
            setEmailError(emailValidationError);
            return;
        }

        if (namaValidationError) {
            setNamaError(namaValidationError);
            return;
        }

        if (nomorHpValidationError) {
            setPhoneError(nomorHpValidationError);
            return;
        }

        // Ensure all fields are filled
        if (!publisherId || !publisherName || !publisherEmail || !publisherPhone) {
            alert("All fields are required");
            return;
        }
        setShowConfirmation(true);
    };

    return (
        <div className="p-4 xl:p-6 mb-32 mt-20 w-full">
            <div className="mb-5">
                <Link href={"/admindashboard/publishers"} className="flex items-center hover:underline font-sans">
                    <IoCaretBackOutline /> Input Data Publisher
                </Link>
            </div>
            <div className="w-full flex justify-start items-center mb-5">
                <span className="font-sans font-bold text-xl">Tambah Publisher</span>
            </div>
            <div className="w-full max-h-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                <form
                    className="bg-white border border-gray-400 p-4 w-full"
                    onSubmit={handleFormSubmit}
                >
                    <label htmlFor="publisherId" className="block text-sm text-gray-700 font-sans mb-2">
                        Id publisher:
                    </label>
                    <input
                        id="publisherId"
                        type="text"
                        onChange={(e) => setPublisherId(e.target.value)}
                        value={publisherId}
                        placeholder="e.g: 1231"
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    <label htmlFor="publisherName" className="mt-4 block text-sm text-gray-700 font-sans mb-2">
                        Nama publisher:
                    </label>
                    <input
                        id="publisherName"
                        type="text"
                        onChange={(e) => { 
                            setPublisherName(e.target.value);
                            setNamaError("");
                        }}
                        value={publisherName}
                        placeholder="e.g: Tere Liye"
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    {namaError && (
                        <p className="text-red-500 text-xs mt-1">{namaError}</p>
                    )}
                    <label htmlFor="publisherEmail" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Email publisher:
                    </label>
                    <input
                        id="publisherEmail"
                        type="text"
                        onChange={(e) => {
                            setPublisherEmail(e.target.value);
                            setEmailError("");
                        }}
                        value={publisherEmail}
                        placeholder="e.g: xxxx@xxx.xx"
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    {emailError && (
                        <p className="text-red-500 text-xs mt-1">{emailError}</p>
                    )}
                    <label htmlFor="publisherPhone" className="block text-sm text-gray-700 font-sans mt-4 mb-2">
                        Nomor Hp publisher:
                    </label>
                    <input
                        id="publisherPhone"
                        type="text"
                        onChange={(e) => {
                            setPublisherPhone(e.target.value);
                            setPhoneError("");
                        }}
                        value={publisherPhone}
                        placeholder="e.g: 08..."
                        className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-gray-400"
                    />
                    {phoneError && (
                        <p className="text-red-500 text-xs mt-1">{phoneError}</p>
                    )}
                    <div className="flex justify-end">
                        <Link href="/admindashboard/publishers">
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
                            Do you want to add this publisher with the provided details?
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

export default CreatePagePublisher;
