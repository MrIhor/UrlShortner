import './App.css';
import HomePage from './Components/HomePage/HomePage';
import { Route, Routes } from 'react-router-dom';
import LinkDetails from './Components/LinkDetails/LinkDetails';
import LoginPage from './Components/LoginPage/LoginPage';
import RegisterPage from './Components/RegisterPage/RegisterPage';
import 'bootstrap-icons/font/bootstrap-icons.css';
import About from './Components/About/About';

function App() {
    return (
        <>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/about" element={<About />} />
                <Route path="/details" element={<LinkDetails />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/register" element={<RegisterPage />} />
            </Routes>
        </>
    );
}

export default App;
