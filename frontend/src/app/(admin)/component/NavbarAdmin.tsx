import Image from "next/image"

const NavbarAdmin = () => {
    return (
        <div className="bg-white shadow-sm fixed w-[86%] md:w-[92%] lg:w-[84%] xl:w-[86%]">
            <div className="flex items-center justify-between p-4">
                <div className="">
                    Dashboard Page
                </div>
                <div className="flex">
                    <div className="hidden md:flex items-center gap-2 text-xs rounded-full ring-[1.5px] ring-gray-200 px-3">
                        <Image src="/search.png" alt="" width={14} height={14}></Image>
                        <input type="text" placeholder="Search...." className="w-[200px] p-2 bg-transparent outline-none" />
                    </div>

                    <div className="flex items-center gap-6 justify-end w-full">
                        <div className="bg-black rounded-full w-10 h-10 flex items-center justify-center cursor-pointer">
                            <Image src={"/message.png"} alt="" width={14} height={14}></Image>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default NavbarAdmin