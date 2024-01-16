import React from "react";

interface SearchInputProps {
    search: string
    setSearch: (value: string) => void;
    handleSearch: () => void;
    disabled: boolean
}

const SearchInput: React.FC<SearchInputProps> = ({ search, handleSearch, setSearch, disabled }) =>
    <div className="input-container">
        <input 
        placeholder="Enter Postal Address..."
        type="text" value={search} className="input-text" onChange={(e) => setSearch(e.target.value)}/>
        <button onClick={handleSearch} className="search-button" disabled={disabled}>
            Search
        </button>
    </div>

export default SearchInput;