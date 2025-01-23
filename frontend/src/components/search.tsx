import { IoSearch } from "react-icons/io5"

const Search = () => {
    // const handleSearch = () => {
    //     console.log("Searching...");
    // };

    return (
        <div className="relative flex flex-1">
            <input
                type="text"
                className="w-full border border-gray-200 py-2 pl-10 text-sm outline-2 rounded-sm"
                placeholder="Search..."
            />
            <IoSearch className="absolute left-3 top-2 h-5 w-5 xl:left-3 text-gray-500" />
            <button
                // onClick={handleSearch}
                className="ml-2 px-4 py-2 text-sm text-white bg-blue-500 rounded-sm hover:bg-blue-600 focus:outline-none"
            >
                Search
            </button>
        </div>
    );
};

export default Search;