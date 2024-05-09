export default function ControlledInput({ placeholder, className='controlled-input', value, changeHandler }) {
  return (
    <input
      type="text"
      value={value}
      onChange={changeHandler}
      className={className}
      placeholder={placeholder}
    />
  );
}
