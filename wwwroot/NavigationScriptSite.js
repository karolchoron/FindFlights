function hideNavbar() {
  var navbar = document.querySelector('.navbar-collapse.show');
  if (navbar) {
    new bootstrap.Collapse(navbar, { toggle: false }).hide();
  }
}
