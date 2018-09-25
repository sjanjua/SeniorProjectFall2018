package com.seniorproject.sarimjanjua.inventorymanagementsystem

import android.content.Intent
import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Button

class MainActivity : AppCompatActivity()
{
    override fun onCreate(savedInstanceState: Bundle?)
    {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val btn : Button = findViewById( R.id.loginButton )


    }

    fun loginButtonClicked( view : View) : Unit
    {
        val intent = Intent( this, HomePage::class.java )

        startActivity( intent )
    }
}
