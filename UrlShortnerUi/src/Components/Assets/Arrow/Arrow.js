import React from 'react';
import { Link } from 'react-router-dom';
import './Arrow.css';

export default function Arrow() {
    return (
        <div className="Arrow">
            <Link to="/">
                <i className="bi bi-arrow-left"></i>
            </Link>
        </div>
    );
}
