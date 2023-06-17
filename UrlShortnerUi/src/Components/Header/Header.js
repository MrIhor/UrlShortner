import React, { useContext, useEffect, useState } from 'react';
import './Header.css';
import { Link } from 'react-router-dom';
import { JwtTokenContext } from '../../contexts/TokenContext';

export default function Header() {
    const [isLogged, setIsLogged] = useState(false);
    const { updateContextValue } = useContext(JwtTokenContext);

    useEffect(() => {
        checkLogged();
    }, []);

    const checkLogged = () => {
        const token = localStorage.getItem('token');
        if (token) {
            setIsLogged(true);
        }
    };

    const logOut = () => {
        localStorage.removeItem('token');
        updateContextValue(null);
        setIsLogged(false);
    };

    return (
        <>
            <div className="HeaderContainer">
                <div className="LogoContainer">
                    <h1>Url Shortner</h1>
                    <Link to="/about" className="AboutLink">
                        About
                    </Link>
                </div>
                <div className="UserContainer">
                    {!isLogged && <Link to="login">Sign In</Link>}
                    {!isLogged && <Link to="register">Sign Up</Link>}
                    <h4>{!isLogged ? 'Not Authenticated' : 'Authenticated'}</h4>
                    {!isLogged ? null : (
                        <button className="LogOutButton" onClick={logOut}>
                            Log Out
                        </button>
                    )}
                </div>
            </div>
        </>
    );
}
