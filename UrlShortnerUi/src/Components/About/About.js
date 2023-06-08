import React, { useEffect, useState } from 'react';
import './About.css';
import axios from 'axios';

export default function About() {
    const [description, setDescription] = useState('');
    const [inputDescription, setInputDescription] = useState('');
    const token = localStorage.getItem('token');
    const [isAdmin, setIsAdmin] = useState(false);

    useEffect(() => {
        getDescription();
        isAdminUser();
    }, []);

    const getDescription = async () => {
        try {
            const { data } = await axios.get('http://localhost:5184/GetAbout');

            setDescription(data.description);
        } catch (error) {
            console.error(error);
        }
    };

    const isAdminUser = async () => {
        try {
            const { data } = await axios.get(
                `http://localhost:5184/GetUserFromToken?AccessToken=${token}`
            );

            if (data.data.role === 'Admin') {
                setIsAdmin(true);
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleDescriptionChange = (event) => {
        setInputDescription(event.target.value);
    };

    const handleSubmitDescription = async (event) => {
        event.preventDefault();

        if (inputDescription !== '') {
            const response = await axios.post(
                `http://localhost:5184/UpdateAbout?Id=${1}&Description=${inputDescription}`,
                null,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                }
            );

            setDescription(response.data.description);
            setInputDescription('');
        }
    };

    return (
        <div className="AboutContainer">
            <h2>About</h2>
            {isAdmin ? (
                <form
                    onSubmit={handleSubmitDescription}
                    className="DescriptionForm"
                >
                    <textarea
                        value={inputDescription}
                        onChange={handleDescriptionChange}
                        className="DescriptionInput"
                    />
                    <input type="submit" className="DescriptionSubmit" />
                </form>
            ) : null}

            <p>{description}</p>
        </div>
    );
}
