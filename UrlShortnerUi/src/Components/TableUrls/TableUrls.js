import React from 'react';
import './TableUrls.css';
import axios from 'axios';
import { Link } from 'react-router-dom';

export default function TableUrls({ linksData, onDeleteUrl }) {
    const token = localStorage.getItem('token');
    const deleteUrl = async (id) => {
        try {
            await axios.delete(`http://localhost:5184/Url/${id}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            onDeleteUrl(id);
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className="TableView">
            <table>
                <thead>
                    <tr>
                        <th>
                            <h3>Base Url</h3>
                        </th>
                        <th>
                            <h3>Short Url</h3>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {linksData &&
                        linksData.map((item) => (
                            <tr key={item.id}>
                                <td>{item.baseUrl}</td>
                                <td>
                                    <a
                                        href={item.shortUrl}
                                        rel="noreferrer"
                                        target="_blank"
                                    >
                                        {item.shortUrl}
                                    </a>
                                </td>
                                <td>
                                    <div>
                                        {token && (
                                            <Link
                                                to="/details"
                                                state={{ id: item.id }}
                                                className="Details"
                                            >
                                                Details
                                            </Link>
                                        )}
                                    </div>
                                </td>
                                <td>
                                    <button
                                        name="delete"
                                        onClick={() => deleteUrl(item.id)}
                                    >
                                        Delete
                                    </button>
                                </td>
                            </tr>
                        ))}
                </tbody>
            </table>
        </div>
    );
}
