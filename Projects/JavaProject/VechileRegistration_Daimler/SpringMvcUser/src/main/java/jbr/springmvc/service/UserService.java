package jbr.springmvc.service;

import org.springframework.stereotype.Service;

import jbr.springmvc.model.Login;
import jbr.springmvc.model.User;

@Service
public interface UserService {

  void register(User user);

  User validateUser(Login login);
}
