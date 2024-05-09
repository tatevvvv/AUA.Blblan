export default function ChatSidebarItem({ text, iconLeft, iconRight, onClick }) {
  return (
    <div onClick={onClick} className="sidebar-item">
      <div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
        {iconLeft && iconLeft}
        <span>{text}</span>
      </div>
      {iconRight && iconRight}
    </div>
  );
}
