use inventory;

DROP TRIGGER IF EXISTS trig_add_order_detail;

DELIMITER $$

CREATE TRIGGER trig_add_order_detail AFTER INSERT ON orderdetails
FOR EACH ROW
BEGIN
	update product
    set unitsinstock = unitsinstock - New.quantity,
		unitsonorder = unitsonorder + New.quantity
	where productid = New.productid;
END $$
DELIMITER ;


DROP TRIGGER IF EXISTS trig_delete_order_detail;

DELIMITER $$
CREATE TRIGGER trig_delete_order_detail AFTER DELETE ON orderdetails
FOR EACH ROW
BEGIN
  update product
    set unitsinstock = unitsinstock + old.quantity,
		unitsonorder = unitsonorder - old.quantity
	where productid = old.productid;
END $$

DELIMITER ;