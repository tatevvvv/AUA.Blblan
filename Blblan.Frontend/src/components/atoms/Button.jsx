import Button from "@mui/material/Button";

export default function ButtonBase({
  color = "#fff",
  children,
  backgroundColor,
  size = "large",
  textColor,
  helperText = "some text",
}) {
  return (
    <Button
      size={size}
      variant="contained"
      label={helperText}
      style={
        backgroundColor
          ? { backgroundColor, color: textColor || color, width: "100%" }
          : { color: textColor || color, width: "100%" }
      }
    >
      {children}
    </Button>
  );
}
