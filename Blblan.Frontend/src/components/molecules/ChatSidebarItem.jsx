import React from 'react';
import PropTypes from 'prop-types';

export default function ChatSidebarItem({ text, iconLeft, iconRight, onClick, isSelected }) {
  return (
    <div onClick={onClick} className={`sidebar-item ${isSelected ? 'selected' : ''}`}>
      <div style={{ display: "flex", alignItems: "center", gap: "10px" }} className="sidebar-item-text">
        {iconLeft && iconLeft}
        <span style={{ marginLeft: iconLeft ? '10px' : '0px' }}>{text}</span>
      </div>
      {iconRight && iconRight}
    </div>
  );
}

ChatSidebarItem.propTypes = {
  text: PropTypes.string.isRequired,
  iconLeft: PropTypes.node,
  iconRight: PropTypes.node,
  onClick: PropTypes.func,
  isSelected: PropTypes.bool,
};
