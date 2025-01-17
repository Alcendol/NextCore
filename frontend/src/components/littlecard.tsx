import { IoSearch } from "react-icons/io5"
import { IoNotificationsCircleSharp } from "react-icons/io5"
import Image from "next/image"
import React from "react";

const LittleCard = () => {
    return (
        <div className="mt-5 mx-2">
            <div className="relative rounded-lg shadow-md p-5 bg-gray-100">
                <span className="flex justify-center font-sans font-bold">Judul Buku</span>
                <div className="relative mt-2">
                    <Image
                        src={"/buku1.png"}
                        alt="contoh buku"
                        width={200}
                        height={200}
                        className="rounded-lg lg:w-full"
                    />
                    <div className="absolute inset-0 flex items-center justify-center bg-black bg-opacity-75 text-white text-center opacity-0 hover:opacity-100 transition-opacity duration-300 rounded-lg">
                        <p className="text-sm p-4">
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent varius
                            nisi ut turpis suscipit, id elementum nisi fermentum.
                        </p>
                    </div>
                </div>
                <div className="w-full h-12">
                    <button className="rounded-lg mt-2 w-full h-full bg-gray-900 text-white hover:text-gray-900 hover:bg-white border hover:border-gray-900">
                        Open Book
                    </button>
                </div>
            </div>
        </div>
    )
}

export default LittleCard