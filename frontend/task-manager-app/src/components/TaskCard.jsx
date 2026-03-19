import React from 'react';
import '../styles/cards.css';

const TaskCard = ({ task, onEdit, onDelete, onToggleComplete }) => {
  return (
    <div className="card">
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
        <h3 className={`card-title ${task.isCompleted ? 'completed' : ''}`}>
          {task.title}
        </h3>
        {task.isCompleted && <span className="card-badge">Completada</span>}
      </div>
      
      <p className="card-desc">{task.description}</p>
      
      <div className="card-meta">
        <div><strong>Inicio:</strong> {new Date(task.start).toLocaleDateString()}</div>
        <div><strong>Fin:</strong> {new Date(task.end).toLocaleDateString()}</div>
      </div>
      
      <div className="card-actions">
        <button 
          onClick={() => onToggleComplete(task)} 
          className="btn-secondary"
        >
          {task.isCompleted ? 'Desmarcar' : 'Completar'}
        </button>
        <button onClick={() => onEdit(task)} className="btn-primary">
          Editar
        </button>
        <button onClick={() => onDelete(task.id)} className="btn-danger">
          Eliminar
        </button>
      </div>
    </div>
  );
};

export default TaskCard;
