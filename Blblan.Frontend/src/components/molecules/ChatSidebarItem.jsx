export default function ChatSidebarItem({ text, iconLeft, iconRight, onClick }) {
  return (
    <div onClick={onClick} className="sidebar-item">
      <div style={{ display: "flex", alignItems: "center", gap: "10px" }}  className="sidebar-item-text">
        {iconLeft && iconLeft}
        <span style={{ marginLeft: iconLeft ? '10px' : '0px' }}>{text}</span>
      </div>
      {iconRight && iconRight}
    </div>
  );
}
