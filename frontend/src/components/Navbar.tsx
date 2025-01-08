import Image from "next/image"

const Navbar = () => {
    return (
        <div className="flex items-center justify-between p-4">
            <div className="hidden xl:flex items-center gap-2 text-xs rounded-full ring-[1.5px] ring-gray-200 px-3">
                <Image src="/search.png" alt="" width={14} height={14}></Image>
                <input type="text" placeholder="Search...." className="w-[200px] p-2 bg-transparent outline-none" />
            </div>
            <div className="hidden"></div>

            {/* <div className="flex items-center gap-6 justify-end w-full">
                <div className="bg-white rounded-full w-7 h-7 flex items-center justify-center cursor-pointer">
                    <Image src={"/message.png"} alt="" width={14} height={14}></Image>
                </div>
            </div> */}
        </div>
    )
}

export default Navbar