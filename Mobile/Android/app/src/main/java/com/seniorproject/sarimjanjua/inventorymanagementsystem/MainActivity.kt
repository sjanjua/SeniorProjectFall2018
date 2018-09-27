package com.seniorproject.sarimjanjua.inventorymanagementsystem

import android.content.Intent
import android.support.v7.app.AppCompatActivity
import android.os.Bundle

import android.support.constraint.ConstraintLayout
import android.widget.Button
import android.view.View


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

