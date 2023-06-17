import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import './RegisterPage.css';
import Arrow from '../Assets/Arrow/Arrow';
import { JwtTokenContext } from '../../contexts/TokenContext';

export default function RegisterPage() {
    const navigate = useNavigate();
    const [user, setUser] = useState({
        username: '',
        email: '',
        password: '',
    });
    const { updateContextValue } = useContext(JwtTokenContext);

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (user.username === '' || user.email === '' || user.password === '') {
            return null;
        } else {
            const { data } = await axios.post(
                `http://localhost:5184/Register?Username=${user.username}&Email=${user.email}&Password=${user.password}`
            );

            const { username, password } = data.data;

            const tokenResponse = await axios.get(
                `http://localhost:5184/GetToken?Username=${username}&Password=${password}`
            );

            if (!tokenResponse.data.data) {
                return null;
            } else {
                localStorage.setItem(
                    'token',
                    tokenResponse.data.data.accessToken
                );
                setUser({
                    username: '',
                    password: '',
                });
                updateContextValue(localStorage.getItem('token'));
                navigate('/');
            }
        }
    };

    const handleInputChange = (event) => {
        setUser({ ...user, [event.target.name]: event.target.value });
    };

    return (
        <div className="RegisterContainer">
            <Arrow />
            <div className="RegisterForm">
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
                    <label>Email</label>
                    <input
                        type="email"
                        placeholder="Email"
                        name="email"
                        value={user.email}
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
                    <button type="submit">Register</button>
                </form>
            </div>
        </div>
    );
}
