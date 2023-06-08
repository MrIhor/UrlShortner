import React, { useEffect, useState } from 'react';
import './Header.css';
import { Link } from 'react-router-dom';

export default function Header() {
    const [isLogged, setIsLogged] = useState(false);

    useEffect(() => {
        checkLogged();
    });

    const checkLogged = () => {
        const token = localStorage.getItem('token');
        if (token) {
            setIsLogged(true);
        }
    };

    const logOut = () => {
        localStorage.removeItem('token');
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
