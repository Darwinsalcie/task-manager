import React from 'react';
import '../styles/cards.css';

const UserCard = ({ user, onDelete }) => {
  return (
    <div className="card">
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
        <h3 className="card-title">
          {user.name}
        </h3>
        <span className={`card-badge ${user.userRole === 0 ? 'admin' : ''}`}>
          {user.userRole === 0 ? 'Admin' : 'User'}
        </span>
      </div>
      
      <p className="card-desc" style={{ marginBottom: '2rem' }}>
        {user.email}
      </p>
      
      <div className="card-actions">
        <button onClick={() => onDelete(user.id)} className="btn-danger">
          Eliminar Usuario
        </button>
      </div>
    </div>
  );
};

export default UserCard;
