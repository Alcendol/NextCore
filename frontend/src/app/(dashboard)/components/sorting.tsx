import { useState } from 'react';

const SortingBuku = () => {
    const [showGenreCheckbox, setShowGenreCheckbox] = useState(false);
    const [showYearCheckbox, setShowYearCheckbox] = useState(false);
    const [showLanguageCheckbox, setShowLanguageCheckbox] = useState(false);

    const toggleGenreCheckbox = () => {
        setShowGenreCheckbox(!showGenreCheckbox);
    };

    const toggleYearCheckbox = () => {
        setShowYearCheckbox(!showYearCheckbox);
    };

    const toggleLanguageCheckbox = () => {
        setShowLanguageCheckbox(!showLanguageCheckbox);
    };

    const stopPropagation = (event: { stopPropagation: () => void; }) => {
        event.stopPropagation();
    };

    return (
        <div className="m">
            <div className="bg-white rounded-lg border border-gray-200 shadow-md dark:bg-gray-800 dark:border-gray-700 mt-4 lg:ml-6 mr-6 lg:mt-8 p-5 hidden md:block">
                <div className="block">
                    <div className="mt-5 cursor-pointer" onClick={toggleGenreCheckbox}>
                        <span className="font-sans font-bold">Genre</span>
                        {showGenreCheckbox && (
                            <div className="mt-2" onClick={stopPropagation}>
                                <input type="checkbox" id="genre1" name="genre1" className="mr-2" />
                                <label htmlFor="genre1">Genre 1</label><br />
                                <input type="checkbox" id="genre2" name="genre2" className="mr-2" />
                                <label htmlFor="genre2">Genre 2</label><br />
                                {/* Add more checkboxes as needed */}
                            </div>
                        )}
                    </div>
                    <div className="mt-5 cursor-pointer" onClick={toggleYearCheckbox}>
                        <span className="font-sans font-bold">Tahun</span>
                        {showYearCheckbox && (
                            <div className="mt-2" onClick={stopPropagation}>
                                <input type="checkbox" id="year2023" name="year2023" className="mr-2" />
                                <label htmlFor="year2023">2023</label><br />
                                <input type="checkbox" id="year2024" name="year2024" className="mr-2" />
                                <label htmlFor="year2024">2024</label><br />
                                {/* Add more checkboxes as needed */}
                            </div>
                        )}
                    </div>
                    <div className="mt-5 cursor-pointer" onClick={toggleLanguageCheckbox}>
                        <span className="font-sans font-bold">Language</span>
                        {showLanguageCheckbox && (
                            <div className="mt-2" onClick={stopPropagation}>
                                <input type="checkbox" id="languageEN" name="languageEN" className="mr-2" />
                                <label htmlFor="languageEN">English</label><br />
                                <input type="checkbox" id="languageID" name="languageID" className="mr-2" />
                                <label htmlFor="languageID">Indonesian</label><br />
                                {/* Add more checkboxes as needed */}
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    )
}

export default SortingBuku;
