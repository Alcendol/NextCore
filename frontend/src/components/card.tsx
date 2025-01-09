import { IoSearch } from "react-icons/io5"
import { IoNotificationsCircleSharp } from "react-icons/io5"
import Image from "next/image"

const Card = () => {
    return (
        <section className="bg-white dark:bg-gray-900 rounded-lg">
            <div className="py-4 mx-auto w-full lg:py-8 lg:px-6">
                <div className="grid gap-8 ">
                <article
                    key={"slug"}
                    className="p-6 bg-white rounded-lg border border-gray-200 shadow-md dark:bg-gray-800 dark:border-gray-700"
                    >
                    <div className="flex flex-col md:flex-row gap-4">
                        {/* Image Section */}
                        <div className="flex-shrink-0">
                        <Image src="/buku1.png" alt="Book Cover"
                            width={200}
                            height={200}
                            className="rounded-lg"
                        />
                        </div>

                        <div className="flex flex-col flex-1 justify-between">
                            <div className="flex justify-between">
                                <h2 className="mb-2 text-lg md:text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                                    <a href={`/home`} className="hover:underline">
                                        Title
                                    </a>
                                    <span> - </span>
                                    <a href={`/home`} className="hover:underline font-light text-gray-500 text-lg md:text-xl">
                                        Author
                                    </a>
                                </h2>
                                <span className="text-sm text-gray-500 dark:text-gray-400">
                                    Published At
                                </span>
                            </div>
                            <div className="">
                                <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Doloremque similique deserunt inventore voluptates quo sit recusandae a neque. Est quisquam quia impedit ad magni libero esse neque eaque dolorum accusantium rem, maiores optio illum? Iure a impedit, deserunt, harum commodi inventore magnam et doloremque reprehenderit aspernatur corrupti labore blanditiis nostrum!</p>
                                <span className="bg-blue-500 text-white text-xs font-medium inline-flex items-center px-2.5 py-1.5 mt-2 rounded mr-3">
                                    <a href={`/home`} className="hover:underline">
                                        Genre
                                    </a>
                                </span>
                                <span className="bg-yellow-500 text-white text-xs font-medium inline-flex items-center px-2.5 py-1.5 mt-2 rounded mr-3">
                                    <a href={`/home`} className="hover:underline">
                                        Genre
                                    </a>
                                </span>
                                <span className="bg-orange-500 text-white text-xs font-medium inline-flex items-center px-2.5 py-1.5 mt-2 rounded mr-3">
                                    <a href={`/home`} className="hover:underline">
                                        Genre
                                    </a>
                                </span>
                                <span className="bg-red-500 text-white text-xs font-medium inline-flex items-center px-2.5 py-1.5 mt-2 rounded mr-3">
                                    <a href={`/home`} className="hover:underline">
                                        Genre
                                    </a>
                                </span>
                            </div>
                            <div className="mt-4">
                                <IoNotificationsCircleSharp className="w-10 h-10 text-yellow-500"/>
                            </div>

                            {/* Author */}
                            <div className="flex mt-4">
                                <a href={`/home`} className="text-sm font-medium hover:underline mr-5">
                                    <button className="rounded-sm bg-blue-500 text-white w-full px-5 h-10">Read More</button>
                                </a>

                                <a href={`/home`} className="text-sm font-medium hover:underline">
                                    <button className="rounded-sm bg-yellow-500 text-black w-full px-5 h-10">Add to Cart <span className="w-2 h-2 rounded-full border border-black px-1">+</span></button>
                                </a>
                            </div>
                        </div>
                    </div>
                    </article>
                </div>
            </div>
        </section>
    )
}

export default Card