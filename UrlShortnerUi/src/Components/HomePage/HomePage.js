import React, { useEffect, useState, useContext } from 'react';
import TableUrls from '../TableUrls/TableUrls';
import axios from 'axios';
import './HomePage.css';
import Header from '../Header/Header';
import { useNavigate } from 'react-router-dom';
import { JwtTokenContext } from '../../contexts/TokenContext';

export default function HomePage() {
    const [links, setLinks] = useState([]);
    const [formData, setFormData] = useState({ url: '' });
    const [showMessage, setshowMessage] = useState(false);
    const { token } = useContext(JwtTokenContext);
    const navigate = useNavigate();

    useEffect(() => {
        fetchLinksData();
    }, []);

    const fetchLinksData = async () => {
        try {
            const { data } = await axios.get(
                'http://localhost:5184/Url/GetAll'
            );
            setLinks(data.data);
        } catch (error) {
            console.log(error);
        }
    };

    const handleChange = (event) => {
        setFormData({ url: event.target.value });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (token) {
            try {
                const response = await axios.post(
                    'http://localhost:5184/Url',
                    formData,
                    {
                        headers: {
                            Authorization: `Bearer ${token}`,
                        },
                    }
                );

                if (!response.data.data) {
                    setshowMessage(true);
                    setFormData({ url: '' });
                } else {
                    const { data } = await axios.get(
                        'http://localhost:5184/Url/GetAll'
                    );

                    setLinks(data.data);
                    setFormData({ url: '' });
                    setshowMessage(false);
                }
            } catch (error) {
                console.error(error);
            }
        } else {
            navigate('login');
        }
    };

    const handleDeleteUrl = (id) => {
        setLinks((prevLinks) => prevLinks.filter((item) => item.id !== id));
    };

    return (
        <div>
            <Header />
            <form onSubmit={handleSubmit} className="UrlForm">
                <div className="FormWrapper">
                    <input
                        type="text"
                        value={formData.url}
                        onChange={handleChange}
                        className="UrlInput"
                    />

                    <input
                        type="submit"
                        value="Submit"
                        className="SubmitButton"
                    />
                </div>
            </form>
            {showMessage ? (
                <p className="Message">This URL is already exist</p>
            ) : null}
            <TableUrls linksData={links} onDeleteUrl={handleDeleteUrl} />
        </div>
    );
}
