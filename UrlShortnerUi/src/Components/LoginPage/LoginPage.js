import React, { useState } from 'react';
import './LoginPage.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import Arrow from '../Assets/Arrow/Arrow';

export default function LoginPage() {
    const navigate = useNavigate();
    const [user, setUser] = useState({
        username: '',
        password: '',
    });

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (user.username === '' || user.password === '') {
            return null;
        } else {
            const { data } = await axios.get(
                `http://localhost:5184/GetToken?Username=${user.username}&Password=${user.password}`
            );

            if (!data.data) {
                return null;
            } else {
                localStorage.setItem('token', data.data.accessToken);
                setUser({
                    username: '',
                    password: '',
                });
                navigate('/');
            }
        }
    };

    const handleInputChange = (event) => {
        setUser({ ...user, [event.target.name]: event.target.value });
    };

    return (
        <div className="LoginContainer">
            <Arrow />
            <div className="LoginForm">
                <h2>Login Page</h2>
                <form onSubmit={handleSubmit}>
                    <label>Username</label>
                    <input
                        type="text"
                        placeholder="Username"
                        name="username"
                        value={user.username}
                        onChange={handleInputChange}
                    />
                    <label>Password</label>
                    <input
                        type="password"
                        placeholder="Password"
                        name="password"
                        value={user.password}
                        onChange={handleInputChange}
                    />
                    <div className="AuthorizeButtons">
                        <button type="submit">Login</button>
                        <button onClick={() => navigate('/register')}>
                            Sign Up
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
