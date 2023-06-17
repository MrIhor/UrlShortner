import React, { createContext, useState } from 'react';

export const JwtTokenContext = createContext();

export default function TokenContext({ children }) {
    const [token, setToken] = useState(null);

    const updateContextValue = (newValue) => {
        setToken(newValue);
    };
    return (
        <JwtTokenContext.Provider value={{ token, updateContextValue }}>
            {children}
        </JwtTokenContext.Provider>
    );
}
