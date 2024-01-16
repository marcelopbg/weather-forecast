import React from "react";

interface LoadingProps {
    isLoading: boolean
}

const LoadingOverlay : React.FC<LoadingProps> = ({ isLoading }) => 
isLoading && (
    <>
        <div className="loading-overlay">
            <span className="loader"></span>
        </div>
    </>
)

export default LoadingOverlay;