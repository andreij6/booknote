// /Scripts/HelloWorld.jsx
var HelloWorld = React.createClass({
	render: function () {
		return (
		    <div>Hello {this.props.name}</div>
        );
}
});

ReactDOM.render(
  <HelloWorld name="Andre"/>,
  document.getElementById('content')
);