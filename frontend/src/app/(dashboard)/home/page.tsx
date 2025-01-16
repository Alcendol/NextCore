import Image from "next/image"
import Search from "@/components/search"
import Card from "@/components/card"
import LittleCard from "@/components/littlecard"

const HomePage = () => {
    return (
        <div className="container mx-auto mt-32 pb-5 mb-32">
            <div className="mt-32 mb-32">
                <span className="flex justify-center font-bold text-3xl font-sans text-gray-700 hover:underline mb-5">Peminjaman Buku</span>
                <Search />
                <div className="grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-4 w-full mt-10 xl:mt-0">
                    <LittleCard />
                    <LittleCard />
                    <LittleCard />
                    <LittleCard />
                </div>
            </div>
            <div className="mt-32 mb-32">
                <span className="flex justify-center font-bold text-3xl font-sans text-gray-700 hover:underline mb-5">E-Book</span>
                <Search />
                <div className="grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-4 w-full mt-10 xl:mt-0">
                    <LittleCard />
                    <LittleCard />
                    <LittleCard />
                    <LittleCard />
                </div>
            </div>
            <div className="w-full mb-5 flex flex-col lg:flex-row justify-between items-start">
                <div className="mb-4 lg:mb-0 lg:w-1/2 xl:mr-10">
                    <span className="text-lg text-gray-700 font-sans font-bold block">
                        Taman Baca
                    </span>
                    <p className="text-lg text-gray-500 font-sans text-justify mt-6">
                        Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium, dicta laborum consectetur voluptas iusto rerum laboriosam expedita voluptatibus cum animi veniam omnis! Atque, quae tempora modi iusto unde explicabo pariatur earum recusandae eligendi. Delectus aliquid perspiciatis ea magnam, iste quisquam neque, voluptatibus voluptatum asperiores culpa aliquam illum reiciendis dicta vitae quis sequi? Atque, eius repellat illum officiis earum soluta temporibus!
                    </p>
                    <p className="text-lg text-gray-500 font-sans text-justify mt-6">
                        Lorem ipsum dolor sit amet consectetur adipisicing elit. Praesentium, dicta laborum consectetur voluptas iusto rerum laboriosam expedita voluptatibus cum animi veniam omnis! Atque, quae tempora modi iusto unde explicabo pariatur earum recusandae eligendi. Delectus aliquid perspiciatis ea magnam, iste quisquam neque, voluptatibus voluptatum asperiores culpa aliquam illum reiciendis dicta vitae quis sequi? Atque, eius repellat illum officiis earum soluta temporibus!
                    </p>
                    <div className="mt-6 flex justify-center xl:justify-start xl:items-center">
                        <button className="w-48 h-16 bg-gray-600 text-white hover:text-gray-600 hover:bg-white hover:border hover:border-gray-600">
                            Read More
                        </button>
                    </div>
                </div>
                <div className="grid grid-cols-2 gap-4 sm:grid-cols-3 lg:grid-cols-4 w-full lg:w-1/2 mt-10 xl:mt-0">
                    <Image src="/buku1.png" alt="Book Cover"
                        width={200}
                        height={200}
                        className="rounded-lg"
                    />
                    <Image src="/buku1.png" alt="Book Cover"
                        width={200}
                        height={200}
                        className="rounded-lg"
                    />
                    <Image src="/buku1.png" alt="Book Cover"
                        width={200}
                        height={200}
                        className="rounded-lg"
                    />
                    <Image src="/buku1.png" alt="Book Cover"
                        width={200}
                        height={200}
                        className="rounded-lg"
                    />
                    <Image src="/buku1.png" alt="Book Cover"
                        width={200}
                        height={200}
                        className="rounded-lg"
                    />
                    <Image src="/buku1.png" alt="Book Cover"
                        width={200}
                        height={200}
                        className="rounded-lg"
                    />
                    <Image src="/buku1.png" alt="Book Cover"
                        width={200}
                        height={200}
                        className="rounded-lg"
                    />
                    <Image src="/buku1.png" alt="Book Cover"
                        width={200}
                        height={200}
                        className="rounded-lg"
                    />
                </div>
            </div>
            
            <div className="mt-32 mb-32">
                <span className="text-gray-700 flex justify-end font-bold font-sans text-2xl">What We Provide</span>
                <div className="items-center gap-4 mt-16 grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4">
                    <div className="relative group w-full h-64 lg:w-full lg:h-72 bg-white border border-gray-600 rounded-lg shadow-lg hover:shadow-xl transform hover:scale-105 transition duration-500 animate-on-scroll">
                        <div className="absolute inset-0 flex items-center justify-center text-gray-600 font-bold text-xl transition-all duration-500 group-hover:translate-y-[-20px] group-hover:opacity-0">Warna Kuning</div>
                        <div className="absolute inset-0 flex flex-col items-center justify-center text-center text-gray-800 p-4 opacity-0 translate-y-[20px] transition-all duration-500 group-hover:translate-y-0 group-hover:opacity-100">
                            <h2 className="font-bold text-2xl">Warna Kuning</h2>
                            <p className="text-sm mt-2">Warna kuning melambangkan rasa kebersamaan dan solidaritas di antara individu atau kelompok.</p>
                        </div>
                    </div>
                  
                    <div className="relative group w-full h-64 lg:w-full lg:h-72 bg-white border border-gray-600 rounded-lg shadow-lg hover:shadow-xl transform hover:scale-105 transition duration-500 animate-on-scroll">
                        <div className="absolute inset-0 flex items-center justify-center text-gray-600 font-bold text-xl transition-all duration-500 group-hover:translate-y-[-20px] group-hover:opacity-0">Warna Biru</div>
                        <div className="absolute inset-0 flex flex-col items-center justify-center text-center text-gray-800 p-4 opacity-0 translate-y-[20px] transition-all duration-500 group-hover:translate-y-0 group-hover:opacity-100">
                            <h2 className="font-bold text-2xl">Warna Biru</h2>
                            <p className="text-sm mt-2">Warna biru melambangkan ketenangan dan kedamaian.</p>
                        </div>
                    </div>
                  
                    <div className="relative group w-full h-64 lg:w-full lg:h-72 bg-white border border-gray-600 rounded-lg shadow-lg hover:shadow-xl transform hover:scale-105 transition duration-500 animate-on-scroll">
                        <div className="absolute inset-0 flex items-center justify-center text-gray-600 font-bold text-xl transition-all duration-500 group-hover:translate-y-[-20px] group-hover:opacity-0">Warna Jingga</div>
                        <div className="absolute inset-0 flex flex-col items-center justify-center text-center text-gray-800 p-4 opacity-0 translate-y-[20px] transition-all duration-500 group-hover:translate-y-0 group-hover:opacity-100">
                            <h2 className="font-bold text-2xl">Warna Jingga</h2>
                            <p className="text-sm mt-2">Warna jingga melambangkan semangat dan energi positif.</p>
                        </div>
                    </div>
                    <div className="relative group w-full h-64 lg:w-full lg:h-72 bg-white border border-gray-600 rounded-lg shadow-lg hover:shadow-xl transform hover:scale-105 transition duration-500 animate-on-scroll">
                        <div className="absolute inset-0 flex items-center justify-center text-gray-600 font-bold text-xl transition-all duration-500 group-hover:translate-y-[-20px] group-hover:opacity-0">Warna Jingga</div>
                        <div className="absolute inset-0 flex flex-col items-center justify-center text-center text-gray-800 p-4 opacity-0 translate-y-[20px] transition-all duration-500 group-hover:translate-y-0 group-hover:opacity-100">
                            <h2 className="font-bold text-2xl">Warna Jingga</h2>
                            <p className="text-sm mt-2">Warna jingga melambangkan semangat dan energi positif.</p>
                        </div>
                    </div>
                    <div className="relative group w-full h-64 lg:w-full lg:h-72 bg-white border border-gray-600 rounded-lg shadow-lg hover:shadow-xl transform hover:scale-105 transition duration-500 animate-on-scroll">
                        <div className="absolute inset-0 flex items-center justify-center text-gray-600 font-bold text-xl transition-all duration-500 group-hover:translate-y-[-20px] group-hover:opacity-0">Warna Jingga</div>
                        <div className="absolute inset-0 flex flex-col items-center justify-center text-center text-gray-800 p-4 opacity-0 translate-y-[20px] transition-all duration-500 group-hover:translate-y-0 group-hover:opacity-100">
                            <h2 className="font-bold text-2xl">Warna Jingga</h2>
                            <p className="text-sm mt-2">Warna jingga melambangkan semangat dan energi positif.</p>
                        </div>
                    </div>
                </div>
            </div>
            <div className="mt-32 mb-32">
                <span className="flex justify-center font-bold text-3xl font-sans text-gray-700 hover:underline">Our Partner</span>
                <span className="flex justify-center text-gray-500 font-sans font-thin mt-2">Introduce you our partnership Taman Baca masyarakat (TBM)</span>
                <div className="w-full mb-5 flex flex-col lg:flex-row justify-between items-start mt-10">
                    <div className="mb-4 lg:mb-0 lg:w-1/2 ">
                        <span className="font-semibold font-sans text-gray-700 text-xl">Apa itu TBM?</span>
                        <p className="font-normal font-sans text-gray-600 text-justify mt-5">Taman Baca Masyarakat (TBM) adalah sebuah inisiatif atau tempat yang dibuat dengan tujuan untuk meningkatkan minat baca dan literasi di masyarakat, khususnya di wilayah pedesaan atau perkotaan yang belum memiliki akses mudah ke perpustakaan atau bahan bacaan. TBM sering kali diinisiasi oleh komunitas lokal, LSM, pemerintah daerah, atau individu yang peduli terhadap pendidikan dan literasi.</p>
                        <Image  src="/tamanbaca.jpg"
                            alt="Book Cover"
                            width={500} // Aspect ratio (width/height)
                            height={300}
                            className="rounded-lg mt-5"
                            layout="responsive"
                        />
                    </div>
                    <div className="mb-4 lg:mb-0 lg:w-1/2 xl:ml-10">
                        <span className="font-sans font-bold text-gray-700 block">Manfaat TBM</span>
                        <p className="text-justify font-normal font-sans text-gray-600 mt-5">
                            <span className="font-bold">Akses Terbuka dan Gratis</span>: TBM memberikan akses terbuka dan gratis bagi masyarakat umum untuk meminjam dan membaca berbagai jenis buku dan bahan bacaan, mulai dari buku cerita anak-anak, buku pelajaran, hingga buku referensi dan majalah. 
                        </p>
                        <p className="text-justify font-normal font-sans text-gray-600 mt-5">
                            <span className="font-bold">Akses Terbuka dan Gratis</span>: TBM memberikan akses terbuka dan gratis bagi masyarakat umum untuk meminjam dan membaca berbagai jenis buku dan bahan bacaan, mulai dari buku cerita anak-anak, buku pelajaran, hingga buku referensi dan majalah. 
                        </p>
                        <p className="text-justify font-normal font-sans text-gray-600 mt-5">
                            <span className="font-bold">Akses Terbuka dan Gratis</span>: TBM memberikan akses terbuka dan gratis bagi masyarakat umum untuk meminjam dan membaca berbagai jenis buku dan bahan bacaan, mulai dari buku cerita anak-anak, buku pelajaran, hingga buku referensi dan majalah. 
                        </p>
                        <p className="text-justify font-normal font-sans text-gray-600 mt-5">
                            <span className="font-bold">Akses Terbuka dan Gratis</span>: TBM memberikan akses terbuka dan gratis bagi masyarakat umum untuk meminjam dan membaca berbagai jenis buku dan bahan bacaan, mulai dari buku cerita anak-anak, buku pelajaran, hingga buku referensi dan majalah. 
                        </p>
                        <p className="text-justify font-normal font-sans text-gray-600 mt-5">
                            <span className="font-bold">Akses Terbuka dan Gratis</span>: TBM memberikan akses terbuka dan gratis bagi masyarakat umum untuk meminjam dan membaca berbagai jenis buku dan bahan bacaan, mulai dari buku cerita anak-anak, buku pelajaran, hingga buku referensi dan majalah. 
                        </p>
                        <p className="text-justify font-normal font-sans text-gray-600 mt-5">
                            <span className="font-bold">Akses Terbuka dan Gratis</span>: TBM memberikan akses terbuka dan gratis bagi masyarakat umum untuk meminjam dan membaca berbagai jenis buku dan bahan bacaan, mulai dari buku cerita anak-anak, buku pelajaran, hingga buku referensi dan majalah. 
                        </p>
                        <div className="mt-6 flex justify-center xl:justify-start xl:items-center">
                        <button className="w-full h-16 bg-gray-600 text-white  rounded-lg hover:text-gray-600 hover:bg-white hover:border hover:border-gray-600">
                            Read More
                        </button>
                    </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default HomePage