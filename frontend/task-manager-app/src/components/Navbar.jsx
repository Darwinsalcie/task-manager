import React, { useContext } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import '../styles/navbar.css';

const Navbar = () => {
  const { user, logout } = useContext(AuthContext);
  const navigate = useNavigate();
  const location = useLocation();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  if (!user) return null;

  return (
    <nav className="navbar">
      <Link to="/tasks" className="navbar-brand">
        Task<span>Manager</span>
      </Link>
      <div className="navbar-menu">
        <Link 
          to="/tasks" 
          className={`navbar-link ${location.pathname === '/tasks' ? 'active' : ''}`}
        >
          Mis Tareas
        </Link>
        {user.userRole === 0 && (
          <Link 
            to="/users" 
            className={`navbar-link ${location.pathname === '/users' ? 'active' : ''}`}
          >
            Usuarios
          </Link>
        )}
      </div>
      <div className="navbar-user-info">
        <span className="navbar-username">{user.name}</span>
        <button onClick={handleLogout} className="btn-secondary">
          Cerrar Sesión
        </button>
      </div>
    </nav>
  );
};

export default Navbar;
