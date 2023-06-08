import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import './LinkDetails.css';
import Arrow from '../Assets/Arrow/Arrow';

export default function LinkDetails() {
    const [url, setUrl] = useState({});
    const location = useLocation();
    const token = localStorage.getItem('token');

    useEffect(() => {
        const getUrl = async (id) => {
            try {
                const { data } = await axios.get(
                    `http://localhost:5184/Url/${id}`,
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    }
                );
                setUrl(data.data);
            } catch (error) {
                console.error(error);
            }
        };

        getUrl(location.state.id);
    }, [location.state.id, token]);

    return (
        <div className="LinkDetailsContainer">
            <div className="HeaderDetails">
                <Arrow />
                <h2>Short Url Details</h2>
            </div>

            <ul>
                <li>
                    <span>Base Url:</span>
                    {url.baseUrl}
                </li>
                <li>
                    <span>Created Data:</span>
                    {url.createdDate}
                </li>
            </ul>
        </div>
    );
}
