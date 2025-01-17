"use client";

import CardCart from "@/components/card-cart";
import { SetStateAction, useState } from "react";

const CartPage = () => {
    const [activeCart, setActiveCart] = useState("peminjaman");

    const toggleCartSection = (tab: SetStateAction<string>) => {
        setActiveCart(tab);
    };

    return (
        <div className="block">
            {/* Navigation Tabs */}
            <div className="px-5 flex justify-center items-center">
                <div className="w-full border-gray-600 border-2 px-2 py-2 flex justify-between items-center">
                    

                    <div
                        id="peminjamanBuku"
                        className={`w-1/2 px-4 py-2 cursor-pointer flex justify-center items-center transition ease-in-out duration-300 
                            ${activeCart === "peminjaman" 
                                ? "bg-gray-700 text-white" 
                                : "bg-white text-gray-800"
                            }`}
                        onClick={() => toggleCartSection("peminjaman")}
                    >
                        <h2 className="text-md font-bold">Peminjaman Buku</h2>
                    </div>


                    <div
                        id="historyTab"
                        className={`w-1/2 px-4 py-2 cursor-pointer flex justify-center items-center transition ease-in-out duration-300 
                            ${activeCart === "history" 
                                ? "bg-gray-700 text-white" 
                                : "bg-white text-gray-800"
                            }`}
                        onClick={() => toggleCartSection("history")}
                    >
                        <h2 className="text-md font-bold">History</h2>
                    </div>
                </div>
            </div>

            <div className="mt-5 ease-in-out transition duration-100">
                {activeCart === "peminjaman" && <CardCart />}
                {activeCart === "history" && (
                    <div className="text-center text-gray-700 dark:text-gray-300">
                        <p className="text-lg font-semibold">History of Borrowed Books</p>
                        <p className="mt-2">No history available at the moment.</p>
                    </div>
                )}
            </div>
        </div>
    );
};

export default CartPage;
